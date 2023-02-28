using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyBlog.Models.ViewModels.NavigationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            // получаем IUrlHelperFactory
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageVM PageVM { get; set; }
        public string PageAction
        {
            get; set;
        }
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Получаем объект IUrlHelper для создания ссылки,
            // передав ViewContext, описаный выше
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            // родительский элемент - div
            output.TagName = "div";
            TagBuilder tag = new TagBuilder("ul");
            // бутстрапим классами bootstrap
            tag.AddCssClass("pagination justify-content-center");

            // Начальный элемент "<<" и немного логики по его отображению
            TagBuilder back = CreateTag(" << ", urlHelper); // Метод создания элемента
            if (!PageVM.HasPreviousPage)
            {
                back.AddCssClass("disabled");
            }
            tag.InnerHtml.AppendHtml(back);
            for (int i = 1; i <= PageVM.PageNumber; i++)
            {
                if ((PageVM.PageNumber - 3 <= i) || (i == 1) || (PageVM.PageNumber == i))
                {
                    TagBuilder Item = CreateTag(i.ToString(), urlHelper);
                    tag.InnerHtml.AppendHtml(Item);
                }
            }

            // Здесь формируем ПОСЛЕ текущей страницы
            for (int i = PageVM.PageNumber + 1; i <= PageVM.TotalPages; i++)
            {
                if ((PageVM.PageNumber + 3 >= i) || (i == PageVM.TotalPages) || (PageVM.PageNumber == i))
                {
                    TagBuilder nextItem = CreateTag(i.ToString(), urlHelper);
                    tag.InnerHtml.AppendHtml(nextItem);
                }
            }
            // Конечный элемент >>
            TagBuilder forward = CreateTag(" >> ", urlHelper);
            if (!PageVM.HasNextPage)
            {
                forward.AddCssClass("disabled");
            }
            tag.InnerHtml.AppendHtml(forward);

            output.Content.AppendHtml(tag);
        }


        // Метод создания элемента
        // pageNumber переведен в строки, потому что есть "<<" и ">>"
        TagBuilder CreateTag(string pageNumber, IUrlHelper urlHelper)
        {
            // создаем li
            TagBuilder item = new TagBuilder("li");
            // создаем ссылку
            TagBuilder link = new TagBuilder("a");

            // если выше из for номер текущей страницы -
            // стилизуем ее классом bootstrap - active
            if (pageNumber == this.PageVM.PageNumber.ToString())
            {
                item.AddCssClass("active");
            }
            else
            {
                // если нажали на << то текущую страницу на 1 назад
                if (pageNumber == " << ")
                {
                    PageUrlValues["page"] = PageVM.PageNumber - 1;
                }
                // если >> то на 1 вперед
                else if (pageNumber == " >> ")
                {
                    PageUrlValues["page"] = PageVM.PageNumber + 1;
                }
                // или нажали на конкретную страницу
                else
                {
                    PageUrlValues["page"] = pageNumber;
                }

                // в атрибут href добавляем action (Index) и значения
                // из словаря (смотреть в самон начале закоментированыый tag-helper)
                // выйдет минимум что-то типа как ниже
                // "<a href=\"/Posts?categoryId=0&amp;sortOrder=TitleAsc&amp;page=4\"></a>"
                link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            }
            // бутстрапим
            item.AddCssClass("page-item");
            link.AddCssClass("page-link link-success fw-bold border-bottom border-5 border-info");
            // в ссылку добавляем номер страницы или же << >>
            link.InnerHtml.Append(pageNumber.ToString());
            // в li добавляем ссылку
            item.InnerHtml.AppendHtml(link);
            // возвращаем
            return item;
        }
    }

}
        
