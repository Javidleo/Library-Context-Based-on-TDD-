namespace testProject
{
    /// <summary>
    /// This Interface have Person Service Methods
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// this Method Define Logic 
        /// </summary>
        /// <param name="person"></param>
        void Add(Person person);
        /// <summary>
        /// This one Define Logic 
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="person"></param>
        void Update(int Id, Person person);
        /// <summary>
        /// This Method Define Logic 
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
