using System;
using System.Collections.Generic;
using System.Text;
using WhiteSolution.Models;

namespace WhiteSolution.Transitions
{
    public class BaseTransitional
    {
        public virtual TBaseModel ToModel<TBaseModel>() where TBaseModel : BaseModel, new()
        {
            return new TBaseModel();
        }
    }
}
