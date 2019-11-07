@model $rootnamespace$.Models.Category

@{
    ViewBag.Title = "Details";
}

<h2 class="mb-5">Details</h2>

<div>
    <h4>Category</h4>
    <hr />
    <dl class="row">
        <dt class="col-4">
            @Html.DisplayNameFor(model => model.Name)
        </dt>

        <dd class="col-8">
            @Html.DisplayFor(model => model.Name)
        </dd>

        <dt class="col-4">
            @Html.DisplayNameFor(model => model.Description)
        </dt>

        <dd class="col-8">
            @Html.DisplayFor(model => model.Description)
        </dd>

    </dl>
</div>
<p>
    <a href="@Url.Action("Edit",new { Model.Id })" class="btn btn-warning"><span class="fa fa-pencil"></span> Edit</a>
    @Html.ActionLink("Back to List", "Index", null, new { @class = "btn btn-secondary" })
</p>
