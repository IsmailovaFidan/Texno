@model $rootnamespace$.Models.Category

@{
    ViewBag.Title = "Delete";
}

<h2 class="mb-5">Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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

    @using (Html.BeginForm()) {
        @Html.AntiForgeryToken()

        <div class="form-actions no-color">
            <button type="submit" class="btn btn-danger"><span class="fa fa-trash"></span> Delete</button> @Html.ActionLink("Back to List", "Index", null, new { @class = "btn btn-secondary" })
        </div>
    }
</div>
