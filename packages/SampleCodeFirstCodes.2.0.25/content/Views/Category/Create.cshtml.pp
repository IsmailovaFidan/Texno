@model $rootnamespace$.Models.Category

@{
    ViewBag.Title = "Create";
}

<h2 class="mb-5">Create</h2>

@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
    <div>
        <h4>Category</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        <div class="form-group row row">
            @Html.LabelFor(model => model.Name, htmlAttributes: new { @class = "col-form-label col-md-2 font-weight-bold" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Name, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Name, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group row row">
            @Html.LabelFor(model => model.Description, htmlAttributes: new { @class = "col-form-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Description, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Description, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group row row">
            <div class="offset-md-2 col-md-10">
                <button type="submit" class="btn btn-success"><span class="fa fa-save"></span> Create</button> @Html.ActionLink("Back to List", "Index", null, new { @class="btn btn-secondary" })
            </div>
        </div>
    </div>
}
