using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ILS_TEST_V1
{
    public class Helper1
    {
        public static void CopyProperties<T1, T2>(T1 source, T2 target)
        {
            var properties = source.GetType().GetProperties();
            foreach (var p in properties)
            {
                var value = p.GetValue(source, null);
                var targetProperty = target.GetType().GetProperty(p.Name);
                if (targetProperty != null)
                {
                    if (targetProperty.CanWrite)
                    {
                        targetProperty.SetValue(target, value, null);
                        Console.WriteLine("{0}:{1}", p.Name, value);
                    }
                    else
                    {
                        Console.WriteLine("X-{0}-CanWrite", p.Name);
                    }
                }
                else
                {
                    Console.WriteLine("X-{0}", p.Name);
                }
            }
        }
    }

    // ModelBase 설정
    public class ModelBase<TModel> : INotifyPropertyChanged
    {
        // Allows you to specify a lambda for notify property changed
        public event PropertyChangedEventHandler PropertyChanged;

        // Defined as virtual so you can override if you wish
        protected virtual void NotifyPropertyChanged<TResult>(Expression<Func<TModel, TResult>> property)
        {
            // Convert expression to a property name
            string propertyName = ((MemberExpression)property.Body).Member.Name;

            // Fire notify property changed event
            InternalNotifyPropertyChanged(propertyName);
        }

        protected void InternalNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
