using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WhiteMvvm.Bases;


namespace WhiteMvvm.Utilities
{
    public class TransitionalList<TBaseTransition> : Collection<TBaseTransition> where TBaseTransition : BaseTransitional
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
