using System;
using System.Linq.Expressions;
using System.Reflection;

namespace XamarinFreshMvvm.Helpers
{
    /// <summary>
    ///     Delegate command from Prism Library https://github.com/PrismLibrary/Prism.
    ///     Licensed under Apache License http://www.apache.org/licenses/
    /// </summary>
    public class DelegateCommand<T> : DelegateCommandBase
    {
        private readonly Action<T> _executeMethod;
        private Func<T, bool> _canExecuteMethod;

        /// <summary>
        ///     Initializes a new instance of <see cref="DelegateCommand{T}" />.
        /// </summary>
        /// <param name="executeMethod">
        ///     Delegate to execute when Execute is called on the command. This can be null to just hook up
        ///     a CanExecute delegate.
        /// </param>
        /// <remarks><see cref="CanExecute" /> will always return true.</remarks>
        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, o => true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="DelegateCommand{T}" />.
        /// </summary>
        /// <param name="executeMethod">
        ///     Delegate to execute when Execute is called on the command. This can be null to just hook up
        ///     a CanExecute delegate.
        /// </param>
        /// <param name="canExecuteMethod">Delegate to execute when CanExecute is called on the command. This can be null.</param>
        /// <exception cref="ArgumentNullException">
        ///     When both <paramref name="executeMethod" /> and
        ///     <paramref name="canExecuteMethod" /> ar <see langword="null" />.
        /// </exception>
        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod),
                    "Neither the executeMethod nor the canExecuteMethod delegates can be null.");

            TypeInfo genericTypeInfo = typeof(T).GetTypeInfo();

            // DelegateCommand allows object or Nullable<>.  
            // note: Nullable<> is a struct so we cannot use a class constraint.
            if (genericTypeInfo.IsValueType)
            {
                if (!genericTypeInfo.IsGenericType || !typeof(Nullable<>).GetTypeInfo()
                        .IsAssignableFrom(genericTypeInfo.GetGenericTypeDefinition().GetTypeInfo()))
                {
                    throw new InvalidCastException("T for DelegateCommand<T> is not an object nor Nullable.");
                }
            }

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        ///     Executes the command and invokes the <see cref="Action{T}" /> provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public void Execute(T parameter)
        {
            _executeMethod(parameter);
        }

        /// <summary>
        ///     Determines if the command can execute by invoked the <see cref="Func{T,Bool}" /> provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command to determine if it can execute.</param>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        /// </returns>
        public bool CanExecute(T parameter)
        {
            return _canExecuteMethod(parameter);
        }

        protected override void Execute(object parameter)
        {
            Execute((T)parameter);
        }

        protected override bool CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        /// <summary>
        ///     Observes a property that implements INotifyPropertyChanged, and automatically calls
        ///     DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="TType">The type of the return value of the method that this delegate encapulates</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        public DelegateCommand<T> ObservesProperty<TType>(Expression<Func<TType>> propertyExpression)
        {
            ObservesPropertyInternal(propertyExpression);
            return this;
        }

        /// <summary>
        ///     Observes a property that is used to determine if this command can execute, and if it implements
        ///     INotifyPropertyChanged it will automatically call DelegateCommandBase.RaiseCanExecuteChanged on property changed
        ///     notifications.
        /// </summary>
        /// <param name="canExecuteExpression">The property expression. Example: ObservesCanExecute(() => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        public DelegateCommand<T> ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            Expression<Func<T, bool>> expression =
                Expression.Lambda<Func<T, bool>>(canExecuteExpression.Body, Expression.Parameter(typeof(T), "o"));
            _canExecuteMethod = expression.Compile();
            ObservesPropertyInternal(canExecuteExpression);
            return this;
        }
    }

    public static class PropertySupport
    {
        /// <summary>
        ///     Extracts the property name from a property expression.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression (e.g. p => p.PropertyName)</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="propertyExpression" /> is null.</exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when the expression is:<br />
        ///     Not a <see cref="MemberExpression" /><br />
        ///     The <see cref="MemberExpression" /> does not represent a property.<br />
        ///     Or, the property is static.
        /// </exception>
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            return ExtractPropertyNameFromLambda(propertyExpression);
        }

        /// <summary>
        ///     Extracts the property name from a LambdaExpression.
        /// </summary>
        /// <param name="expression">The LambdaExpression</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="expression" /> is null.</exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when the expression is:<br />
        ///     The <see cref="MemberExpression" /> does not represent a property.<br />
        ///     Or, the property is static.
        /// </exception>
        internal static string ExtractPropertyNameFromLambda(LambdaExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("The expression is not a member access expression.", nameof(expression));

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
                throw new ArgumentException("The member access expression does not access a property.",
                    nameof(expression));

            var getMethod = property.GetMethod;
            if (getMethod.IsStatic)
                throw new ArgumentException("The referenced property is a static property.", nameof(expression));

            return memberExpression.Member.Name;
        }
    }

}
