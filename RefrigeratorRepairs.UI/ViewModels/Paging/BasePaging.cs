using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorRepairs.UI.ViewModels.Paging
{
    public class BasePaging<T>
    {
        public BasePaging()
        {
            Page = 1;
            TakeEntity = 4;
            HowManyShowPageAfterAndBefore = 5;
        }

        public int Page { get; set; }

        public int PageCount { get; set; }

        public int AllEntitiesCount { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int TakeEntity { get; set; }

        public int SkipEntity { get; set; }

        public int HowManyShowPageAfterAndBefore { get; set; }

        public List<T> Entities { get; set; }

        public BasePaging<T> GetCurrentPaging()
        {
            return this;
        }

        public string GetShownEntitiesPagesTitle()
        {
            if (AllEntitiesCount != 0)
            {
                var startItem = 1;
                var endItem = AllEntitiesCount;

                if (EndPage > 1)
                {
                    startItem = (Page - 1) * TakeEntity + 1;
                    endItem = Page * TakeEntity > AllEntitiesCount ? AllEntitiesCount : Page * TakeEntity;
                }

                return $"نمایش {startItem} تا {endItem} از {AllEntitiesCount}";
            }

            return $"0 آیتم";
        }

        public BasePaging<T> Build(int allEntitiesCount)
        {
            var pageCount = Convert.ToInt32(Math.Ceiling(allEntitiesCount / (double)TakeEntity));
            Page = Page;
            AllEntitiesCount = allEntitiesCount;
            TakeEntity = TakeEntity;
            SkipEntity = (Page - 1) * TakeEntity;
            StartPage = Page - HowManyShowPageAfterAndBefore <= 0 ? 1 : Page - HowManyShowPageAfterAndBefore;
            EndPage = Page + HowManyShowPageAfterAndBefore > pageCount ? pageCount : Page + HowManyShowPageAfterAndBefore;
            HowManyShowPageAfterAndBefore = HowManyShowPageAfterAndBefore;
            PageCount = pageCount;
            return this;
        }

        public  BasePaging<T> SetEntities(IQueryable<T> queryable)
        {
            Entities =  queryable
                .Skip(SkipEntity)
                .Take(TakeEntity)
                .ToList();
            return this;
        }
    }
}
