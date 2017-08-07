namespace StandardBank.ConcessionManagement.Interface.Common
{
    /// <summary>
    /// Marshaller interface
    /// </summary>
    public interface IMarshaller
    {
        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        string SerializeObject<T>(T entity);

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T DeserializeObject<T>(string entity);
    }
}
