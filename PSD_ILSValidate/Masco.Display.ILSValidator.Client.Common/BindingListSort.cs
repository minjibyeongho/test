//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;

//namespace Masco.Display.ILSValidator.Client.Common
//{
//    public class MyBindingList : BindingList<LaneInfoVM>
//    {
//        protected override Comparison<LaneInfoVM> GetComparer(PropertyDescriptor prop)
//        {
//            Comparison<LaneInfoVM> comparer;
//            switch (prop.Name)
//            {
//                case "NUMBER":
//                    comparer = new Comparison<LaneInfoVM>(delegate(LaneInfoVM x, LaneInfoVM y)
//                        {
//                            if (x != null)
//                                if (y != null)
//                                    return (x.NUMBER.CompareTo(y.NUMBER));
//                                else
//                                    return 1;
//                            else if (y != null)
//                                return -1;
//                            else
//                                return 0;
//                        });
//                    break;

//                // Implement comparers for other sortable properties here.
//            }
//            return comparer;
//        }
//    }
//}
