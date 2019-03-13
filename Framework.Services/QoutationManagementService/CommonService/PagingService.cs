using Framework.DTOs.CommonDto;
using Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Utils;
using System.Text;

namespace Framework.Services.QoutationManagementService.CommonService
{
    public interface IPagingObject<T, FilterClass> where FilterClass : 
        PagingBaseDto
    {
        IQueryable<T> GetQuery(FilterClass filter);
    }

    public interface IPagingService<T, FilterClass> where FilterClass : PagingBaseDto
    {
        IQueryable<T> GetItems(FilterClass filter);
        int GetNumbersOfPage(FilterClass filter);
        IPagingObject<T, FilterClass> PagingObject { get; set; }
        List<int> GetNumbersShowPage(FilterClass filter);
    }

    public class PagingService<T, FilterClass>
        : IPagingService<T, FilterClass>
         where FilterClass : PagingBaseDto
    {
        static int DEFAULT_NUMBER_OF_ROWS_PER_PAGE = 10;
        static int DEFAULT_NUMBER_OF_ITEM_ROWS_MAX = 50;

        public IPagingObject<T, FilterClass> PagingObject { get; set; }

        public int GetNumbersOfPage(FilterClass filter)
        {
            var numberOfActiveRows = PagingObject.GetQuery(filter).Count();
            var numberOfRowsPerPage = filter.ItemsPerPage;

            if (numberOfRowsPerPage == 0)
                return 0;
            if (numberOfActiveRows % numberOfRowsPerPage == 0)
                return numberOfActiveRows / numberOfRowsPerPage;
            return numberOfActiveRows / numberOfRowsPerPage + 1;
        }

        public IQueryable<T> GetItems( FilterClass filter)
        {
            var query = PagingObject.GetQuery(filter);
            if(filter.ItemsPerPage==0)
            {
                filter.ItemsPerPage = GetItemsPerPage();
            }
            var numberItemsPerPage = filter.ItemsPerPage;

            // sorting
            if(String.IsNullOrEmpty(filter.SortingAction))
            {
                
            }
            else if (filter.SortingAction == "asc" )
            {
                query = query.OrderBy(filter.SortingColumnName);
            }
            else if (filter.SortingAction == "desc")
            {
                query = query.OrderBy(filter.SortingColumnName, false);
            }
            return query.Skip(filter.CurrentPage * numberItemsPerPage).Take(numberItemsPerPage);
        }

        protected virtual int GetItemsPerPage()
        {
            return DEFAULT_NUMBER_OF_ROWS_PER_PAGE;
        }
        protected virtual int GetItemsPerPageMax()
        {
            return DEFAULT_NUMBER_OF_ITEM_ROWS_MAX;
        }

        public List<int> GetNumbersShowPage(FilterClass filter)
        {
            var numberOfRows = new List<int>();
            int itemsPerPage = GetItemsPerPage();
            int itemsPerPageMax = GetItemsPerPageMax();
            int totalItemCount = PagingObject.GetQuery(filter).Count(); 
            for (int i = itemsPerPage; i < itemsPerPageMax; i += itemsPerPage)
            {
                if (i > totalItemCount)
                {
                    numberOfRows.Add(totalItemCount);
                    return numberOfRows;
                }
                else
                {
                    numberOfRows.Add(i);
                }
            }
            return numberOfRows;
        }
    }
}
