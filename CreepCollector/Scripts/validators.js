// Add data annotation client-side validation adapters.
jQuery.validator.addMethod("enforcetrue", function (value, element, param) {
    return element.checked;
});
jQuery.validator.unobtrusive.adapters.addBool("enforcetrue");

jQuery.validator.addMethod("greaterthanzero", function (value, element, param) {
    return $(element).val() > 0;
});
jQuery.validator.unobtrusive.adapters.addBool("greaterthanzero");
