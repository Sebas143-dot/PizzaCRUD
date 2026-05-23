using PizzaCRUD.Models;
using System.Diagnostics.CodeAnalysis;

namespace PizzaCRUD.Utilities
{
    public static class ProjectExtensions
    {
        public static bool IsNullOrNew([NotNullWhen(false)] this Project? project)
        {
            return project is null || project.ID == 0;
        }
    }
}