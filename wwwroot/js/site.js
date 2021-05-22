function setFormValidationErrors(containerID, error) {
  var container = $(containerID);
  container.removeClass("validation-summary-valid");
  container.addClass("validation-summary-errors");

  var list = $(containerID + " > ul");
  list.empty();
  list.append("<li>" + error + "</li>");
}

function invokeAccountFormAsync(formID, validationContainerID) {
  $(formID).submit(function(event) {
    var form = $(this);
    form.validate();
    if (!form.valid()) {
      return;
    }

    event.preventDefault();

    $.ajax({
      type : form.attr("method"),
      url : form.attr("action"),
      data : form.serialize(),
      dataType : 'json',
      success : function(formValidation) {
        if (formValidation["valid"]) {
          window.location.reload();
        } else {
          setFormValidationErrors(validationContainerID,
                                  formValidation["error"]);
        }
      },
    });
  });
}

function showModalForAnchor(modalID) {
  if (window.location.href.includes(modalID)) {
    $(modalID).modal('show');
  }
}

jQuery.ajaxSetup({
  beforeSend : function() { $(".spinner-border").show(); },
  complete : function() { $(".spinner-border").hide(); },
});

$(document).ready(function() {
  showModalForAnchor("#loginModal");
  showModalForAnchor("#registerModal");

  invokeAccountFormAsync("#loginForm", "#loginFormValidationSummary");
  invokeAccountFormAsync("#registerForm", "#registerFormValidationSummary");
});
