<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - ASP.NET MVC CodeFirst Sample App</title>
    <link href="~/content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/content/font-awesome.min.css" rel="stylesheet" />
    <link href="~/content/site.css" rel="stylesheet" type="text/css" />
    <script src="~/scripts/modernizr-2.6.2.js"></script>

    @RenderSection("_Styles", false)
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        @Html.ActionLink("CodeFirst Sample App", "Index", "Home", new { area = "" }, new { @class = "navbar-brand" })
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav mr-auto">
                <li class="nav-item">
                    <a href="@Url.Action("Index","Home")" class="nav-link">
                        <span class="fa fa-home"></span> Home
                    </a>
                </li>
                <li class="nav-item">
                    <a href="@Url.Action("Index","Customer")" class="nav-link">
                        <span class="fa fa-user"></span> Customer
                    </a>
                </li>
                <li class="nav-item">
                    <a href="@Url.Action("Index","Category")" class="nav-link">
                        <span class="fa fa-list"></span> Category
                    </a>
                </li>
                <li class="nav-item">
                    <a href="@Url.Action("Index","Product")" class="nav-link">
                        <span class="fa fa-shopping-basket"></span> Product
                    </a>
                </li>
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <span class="fa fa-navicon"></span> More
                    </a>
                    <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <a class="dropdown-item" href="@Url.Action("About","Home")"><span class="fa fa-user-circle-o"></span> About</a>
                        <a class="dropdown-item" target="_blank" href="http://muratbaseren.wordpress.com/"><span class="fa fa-external-link-square"></span> Blog</a>
                        <a class="dropdown-item" target="_blank" href="https://www.udemy.com/user/kadirmuratbaeren/"><span class="fa fa-external-link-square"></span> Udemy</a>
                        <a class="dropdown-item" target="_blank" href="https://www.youtube.com/kadirmuratbaseren"><span class="fa fa-youtube"></span> Youtube</a>
                        <div class="dropdown-divider"></div>

                    </div>
                </li>
            </ul>
            <form class="form-inline my-2 my-lg-0" method="post" action="@Url.Action("Search")">
                <input class="form-control mr-sm-2" type="search" placeholder="Search" name="keyword" aria-label="Search">
                <button class="btn btn-outline-success my-2 my-sm-0" type="submit" onclick="javascript: $(this).closest('form').submit();">Search</button>
            </form>
        </div>
    </nav>

    <div class="container body-content mt-4">
        @RenderBody()
        <hr />
        <footer>
            <p><span class="fa fa-github"></span> <a href="https://github.com/muratbaseren/Nuget-CodeFirst-Sampler" target="_blank">View on GitHub</a> &copy; @DateTime.Now.Year - ASP.NET MVC CodeFirst Sample App. Designed by <a href="https://muratbaseren.wordpress.com" target="_blank">K.Murat Baseren</a></p>
        </footer>
    </div>

    <script src="~/scripts/jquery-3.3.1.min.js"></script>
    <script src="~/scripts/bootstrap.min.js"></script>

    @RenderSection("_Scripts", false)
</body>
</html>
