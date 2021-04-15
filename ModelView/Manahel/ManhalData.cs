using ManhaleAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManhaleAspNetCore.ModelView.Manahel
{
    public class ManhalData
    {
        public static async Task<List<ManahelVM>> GetAllManahel(ManahelContext context)
        {
            List<ManahelVM> manahelVMs = new List<ManahelVM>();
            foreach (var item in context.manahels.ToList())
            {
                manahelVMs.Add(new ManahelVM()
                {
                    DateCreated = item.DateCreated,
                    DateUpdated = item.DateUpdated,
                    FlowerName = item.FlowerName,
                    Id = item.Id,
                    LocationName = item.LocationName,
                    NickName = item.NickName,
                    Ssn = item.Ssn,
                    ImageManhals = item.ImageManhals,
                });
            }
            return manahelVMs;
        }
    }
}
