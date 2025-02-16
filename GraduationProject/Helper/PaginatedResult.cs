﻿namespace GraduationProject.Helper
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
