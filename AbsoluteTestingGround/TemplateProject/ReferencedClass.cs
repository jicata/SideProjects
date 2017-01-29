namespace TemplateProject
{
    using System;
    using System.Collections.Generic;

    public class ReferencedClass
    {
        private int id;
        public int Id 
        {
            get { return 101; }
            set { this.id = value; }
        }
        public IReadOnlyCollection<string> Strings { get; set; }
        public Func<string, bool> myFunc { get; set; }

    }
}
