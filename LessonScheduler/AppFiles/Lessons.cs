//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LessonScheduler.AppFiles
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lessons
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lessons()
        {
            this.Plans = new HashSet<Plans>();
        }
    
        public int Id { get; set; }
        public int NumberOfLesson { get; set; }
        public string Lesson { get; set; }
        public int Cabinet { get; set; }
        public int IdUser { get; set; }
        public int IdClass { get; set; }
        public int IdDay { get; set; }
    
        public virtual Classes Classes { get; set; }
        public virtual Days Days { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plans> Plans { get; set; }
    }
}
