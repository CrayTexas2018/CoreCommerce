using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class Box
    {
        [Key]
        public int box_id { get; set; }
        
        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface IBoxRepository
    {
        IEnumerable<Box> GetBoxes();
        Box CreateBox(Box box);
        Box GetBox(int box_id);
        void UpdateBox(Box box);
        void DeleteBox(int box_id);
        void Save();
    }

    public class BoxRepository : IBoxRepository
    {
        private ApplicationContext context;

        public BoxRepository (ApplicationContext context)
        {
            this.context = context;
        }

        public Box CreateBox(Box box)
        {
            box.created = DateTime.Now;
            box.updated = DateTime.Now;

            context.Boxes.Add(box);
            Save();
            return box;
        }

        public void DeleteBox(int box_id)
        {
            context.Boxes.Remove(context.Boxes.Find(box_id));
        }

        public Box GetBox(int box_id)
        {
            return context.Boxes.Find(box_id);
        }

        public IEnumerable<Box> GetBoxes()
        {
            return context.Boxes.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateBox(Box box)
        {
            box.updated = DateTime.Now;

            context.Entry(box).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}