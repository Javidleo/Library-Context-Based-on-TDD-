namespace testProject
{
    /// <summary>
    /// Hello this is **Person** From *TestProject*
    /// </summary>
    public class Person
    {
        /// <summary>
        /// This is the Properties of Person Class 
        /// </summary>
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }

        /// <summary>
        /// This is Constructor
        /// </summary>
        private Person(string name, string family)
        {
            this.Name = name;
            this.Family = family;
        }


        /// <summary>
        /// This is the Create Person Method that Goes on Immutable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        public static Person Create(string name, string family)
            => new Person(name, family);

    }
}