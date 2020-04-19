using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Domain.Models
{
    public class UserError
    {
        private UserError(DateTime createdTime, DateTime? updateTime, string notes)
        {
            CreatedTime = createdTime;
            UpdateTime = updateTime;
            Notes = notes;
        }

        public Guid UserErrorId { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public DateTime? UpdateTime { get; private set; }
        public string Notes { get; private set; }

        public static UserError Create(DateTime createdTime, DateTime? updatedTime, string notes)
        {
            return new UserError(createdTime, updatedTime, notes);
        }

        public ExpandoObject ShapeData(string fields)
        {
            List<PropertyInfo> propertyInfos = typeof(UserError)
                .GetProperties()
                .ToList();

            if (!string.IsNullOrEmpty(fields))
            {
                var propertyNames = fields
                    .Split(',')
                    .ToList();
                propertyInfos = propertyInfos
                    .Where(x => propertyNames.Contains(x.Name))
                    .ToList();
            }

            ExpandoObject expando = new ExpandoObject();
            IDictionary<string, object> properties = ((IDictionary<string, object>)expando);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                properties.Add(propertyInfo.Name, propertyInfo.GetValue(this));
            }

            return expando;
        }

        public void Update(DateTime? createdTime, string notes)
        {
            CreatedTime = createdTime ?? CreatedTime;
            UpdateTime = DateTime.Now;
            Notes = notes;
        }
    }
}
