using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WhiteSolution.Models;
using WhiteSolution.Transitions;

namespace WhiteSolution.Utils
{
    public class TransitionList<TBaseTransition> : Collection<TBaseTransition> where TBaseTransition : BaseTransition
    {
        public virtual ObservableRangeCollection<TBaseModel> ToModel<TBaseModel>() where TBaseModel : BaseModel , new()
        {
            var newList = new ObservableRangeCollection<TBaseModel>();
            foreach (var Transition in this)
            {                
                var baseModel = Transition.ToModel<TBaseModel>();
                newList.Add(baseModel);
            }
            return newList;
        }
    }
}
