namespace TemplateProject
{
    using System;
    using System.Collections.Generic;

    public class ReferencedClass
    {
        public int Id { get; set; }
        public IReadOnlyCollection<string> Strings { get; set; }
        public Func<string, bool> myFunc { get; set; }
        
    }
}
