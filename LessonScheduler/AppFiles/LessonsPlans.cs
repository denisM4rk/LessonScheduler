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
    
    public partial class LessonsPlans
    {
        public int Id { get; set; }
        public int IdPlan { get; set; }
        public int IdLesson { get; set; }
    
        public virtual Lessons Lessons { get; set; }
        public virtual Plans Plans { get; set; }
    }
}