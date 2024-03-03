document.addEventListener('DOMContentLoaded', function () {
    var currentPage = '@ViewContext.RouteData.Values["Action"]';
    var links = document.querySelectorAll('#sidebar .nav-link');
    links.forEach(function (link) {
        if (link.getAttribute('asp-action') === currentPage) {
            link.classList.add('active');
        }
    });
});