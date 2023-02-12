using ConAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Models.DTOs
{
    public class ReturnProjectDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime AddDate { get; set; }
        public Boolean Done { get; set; }
        // public User User { get; set; }
        public string FirstName { get; set; }
        public int UserId { get; set; }

        public ReturnProjectDTO(Project project)
        {
            this.Id = project.Id;
            this.Title = project.Title;
            this.Description = project.Description;
            this.Location = project.Location;
            this.AddDate = project.AddDate;
            this.Done = project.Done;
            this.FirstName = project.User.FirstName;
            this.UserId = project.User.Id;
        }
    }
}
