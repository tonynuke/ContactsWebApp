using System;

namespace Shared
{
    /// <summary>
    /// Спецификация.
    /// </summary>
    /// <typeparam name="T">Элемент.</typeparam>
    public abstract class Specification<T>
    {
        /// <summary>
        /// Условие.
        /// </summary>
        /// <returns>True, если условие выполняется.</returns>
        protected abstract Func<T, bool> condition();

        /// <summary>
        /// Определить, что элемент удовлетворяет условию.
        /// </summary>
        /// <param name="entity">Элемент.</param>
        /// <returns>True, если удовлетворяет.</returns>
        public bool IsSatisfiedBy(T entity)
        {
            return this.condition().Invoke(entity);
        }
    }
}