
//-- Start Top Menu Partial Code -->

    //$("#menu-partial").load("partials/headermenu.html", function () {
    //    console.log("Top Menu Loaded.");
    //});

//-- End Top Menu Partial Code -->


//-- START Footer Partial Code -->

//$("#footer-partial").load("partials/footer.html", function () {
//    console.log("Footer Loaded.");
//});

//-- END Footer Partial Code -->


//-- START nav Search Code -->
$("#search-site").on('click', function () {
    $(".search-form").toggle();
    return false;
});
//-- END nav Search Code -->

// --------------------------------------------------------------- MODAL POP-UP SHOW HOME
$(document).ready(function () {
    $("#myModal").modal('show');

    function alignModal() {
        var modalDialog = $(this).find(".modal-dialog");

        // Applying the top margin on modal dialog to align it vertically center
        modalDialog.css("margin-top", Math.max(0, ($(window).height() - modalDialog.height()) / 2));
    }
    // Align modal when it is displayed
    $(".modal").on("shown.bs.modal", alignModal);

    // Align modal when user resize the window
    $(window).on("resize", function () {
        $(".modal:visible").each(alignModal);
    });
});


// ------- LOGIN TYPE CLICK
$("#studentlogin").on('click', function () {
    $("#studentlogin").addClass("logintypeselected");
    $("#instructorlogin").removeClass("logintypeselected");
    $("#instructorlogin").addClass("logintype");
    $("#logintype").val("STUDENT");
});

$("#instructorlogin").on('click', function () {
    $("#instructorlogin").addClass("logintypeselected");
    $("#studentlogin").removeClass("logintypeselected");
    $("#studentlogin").addClass("logintype");
    $("#logintype").val("INSTRUCTOR");
});


// -------------------------------------------------------------- PLACEMENTS SCROLL
if ($(".placements").length > 0) {
    var placements = $(".placementitem");
    var ini = 4;
    var total = $(".placementitem").length;

    $(".placements").empty();

    if (parseInt(total) > 4) {
        $(".placements").append(placements[0].outerHTML).fadeIn(300);
        $(".placements").append(placements[1].outerHTML).fadeIn(300);
        $(".placements").append(placements[2].outerHTML).fadeIn(300);
        $(".placements").append(placements[3].outerHTML).fadeIn(300);
    }

    setInterval(function () {
        $(".placements .placementitem:first-child").fadeOut().remove();

        if (parseInt(ini) <= parseInt(total)) {
            $(".placements").append(placements[ini].outerHTML).fadeIn(500);
        }

        ini = parseInt(ini) + 1;

        if (parseInt(ini) >= parseInt(total)) {
            ini = 0;
        }
    }, 1500)
}



// -------------------------------------------------------------- TESTIMONIALS SCROLL
if ($(".testimonials").length > 0) {
    var testimonials = $(".testimonial");
    var initest = 3;
    var totaltest = $(".testimonial").length;

    $(".testimonials").empty();

    if (parseInt(totaltest) > 3) {
        $(".testimonials").append(testimonials[0].outerHTML).fadeIn(300);
        $(".testimonials").append(testimonials[1].outerHTML).fadeIn(300);
        $(".testimonials").append(testimonials[2].outerHTML).fadeIn(300);
    }

    setInterval(function () {
        $(".testimonials .testimonial:first-child").fadeOut().remove();

        if (parseInt(initest) <= parseInt(totaltest)) {
            $(".testimonials").append(testimonials[initest].outerHTML).fadeIn(500);
        }

        initest = parseInt(initest) + 1;

        if (parseInt(initest) >= parseInt(totaltest)) {
            initest = 0;
        }
    }, 1500)
}



// --------------------------------------------------------------- STUDENT REGISTRATION PAGE
//-- START Student Reg VALIDATIONS Code -->
$("#studentregsubmit").on('click', function () {


    var submitresult = "";
    var errormsg = "";

    submitresult = "";
    errormsg = "";

    // full name validation
    if ($("#FullName").val() == "") {
        $("#FullName").addClass("errorvalidation");
        errormsg += "Full Name is Required<br>";
        submitresult = "error";
    }
    else {
        $("#FullName").removeClass("errorvalidation");
    }

    // email validation
    if ($("#Email").val() == "") {
        $("#Email").addClass("errorvalidation");
        errormsg += "Email is Required<br>";
        submitresult = "error";
    }
    else if ($("#Email").val().length < 8) {
        $("#Email").addClass("errorvalidation");
        errormsg += "Email Id must be minimum of 8 Characters<br>";
        submitresult = "error";
    }
    else{
        var pattern = new RegExp("^[_A-Za-z0-9-]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$");
        var result = pattern.test($("#Email").val());
        if(!result){
            $("#Email").addClass("errorvalidation");
            errormsg += "Email Format is Wrong<br>";
            submitresult = "error";
        }
        else {
            $("#Email").removeClass("errorvalidation");
        }
    }


    // password validation
    if ($("#Password").val() == "") {
        $("#Password").addClass("errorvalidation");
        errormsg += "Password is Required<br>";
        submitresult = "error";
    }
    else if ($("#Password").val().length < 8) {
        $("#Password").addClass("errorvalidation");
        errormsg += "Password length must be minimum 8 Characters<br>";
        submitresult = "error";
    }
    // password confirm
    else if ($("#Password").val() != $("#R_confirmpassword").val()) {
        $("#Password").addClass("errorvalidation");
        $("#R_confirmpassword").addClass("errorvalidation");
        errormsg += "Passwords do not Match<br>";       

        submitresult = "error";
    }
    else {
        $("#Password").removeClass("errorvalidation");
        $("#R_confirmpassword").removeClass("errorvalidation");
        //$("#studentregvalidationmsg").hide();
    }
    // gender validation
    if ($("#Gender").val() == "") {
        $("#Gender").addClass("errorvalidation");
        errormsg += "Gender is Required<br>";
        submitresult = "error";
    }
    else {
        $("#Gender").removeClass("errorvalidation");
    }
    // education validation
    if ($("#Education").val() == "") {
        $("#Education").addClass("errorvalidation");
        errormsg += "Education is Required<br>";
        submitresult = "error";
    }
    else {
        $("#Education").removeClass("errorvalidation");
    }
    // reference validation
    if ($("#WhoReferredUs").val() == "") {
        $("#WhoReferredUs").addClass("errorvalidation");
        errormsg += "Reference is Required<br>";
        submitresult = "error";
    }
    else {
        $("#WhoReferredUs").removeClass("errorvalidation");
    }
    // age validation
    if (!$.isNumeric($("#Age").val())) {
        $("#Age").addClass("errorvalidation");
        errormsg += "Age must be a number<br>";
        submitresult = "error";
    }
    else if ($("#Age").val().length > 2) {
        $("#Age").addClass("errorvalidation");
        errormsg += "Age must be a less than 99<br>";
        submitresult = "error";
    }
    else {
        $("#Age").removeClass("errorvalidation");
    }
    // phone validation
    if ($("#Phone1").val() == '') {
        $("#Phone1").addClass("errorvalidation");
        errormsg += "Phone number is Required<br>";
        submitresult = "error";
    }
    else if ($("#Phone1").val().length < 8 || $("#Phone1").val().length > 12) {
        $("#Phone1").addClass("errorvalidation");
        errormsg += "Phone number must be a 8-12 digits<br>";
        submitresult = "error";
    }
    else {
        var regex = /^[0-9+]*$/;
        isValid = regex.test($("#Phone1").val());
        if (!isValid) {
            $("#Phone1").addClass("errorvalidation");
            errormsg += "Phone number must be a number<br>";
            submitresult = "error";
        }
        else {
            $("#Phone1").removeClass("errorvalidation");
        }
    }

    if (submitresult == "error") {
        $("#studentregvalidationmsg").show();
        $("#studentregvalidationmsg").html(errormsg);

        event.preventDefault();
    }
    else{
        $("#studentregvalidationmsg").hide();
    }
});
//-- END Student Reg Password Check Code -->



// --------------------------------------------------------------- COURSES PAGE

$("#courses2").hide();

$("#pageid1").click(function () {
    $("#courses2").hide();
    $("#courses1").show();

    $("#pageid1").addClass("active");
    $("#pageid2").removeClass("active");
});

$("#pageid2").click(function () {
    $("#courses1").hide();
    $("#courses2").show();

    $("#pageid2").addClass("active");
    $("#pageid1").removeClass("active");
});




// --------------------------------------------------------------- ENQUIRY FORM SUBMIT
$("#enquirysubmit").click(function () {

    event.preventDefault();
    
    var form = $("#contact-form1");
    form.validate();
    if (form.valid()) {

        var url = "./bntstudenthubdata.asmx/enquiry";

        var enquiryname = $("#contact_name").val();
        var enquiryemail = $("#contact_email").val();
        var enquiryphone = $("#contact_number").val();
        var enquirymessage = $("#contact_message").val();

        var data = "{name:\"" + enquiryname + "\"" +
               ", email:\"" + enquiryemail + "\"" +
               ", phonenumber:\"" + enquiryphone + "\"" +
               ", message:\"" + enquirymessage + "\"}";

        console.log(data);

        $.ajax({
            data: data,
            dataType: "json",
            url: url,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                //alert(result.d);
                console.log(result.d);

                $("#success").show();
                $("#success").fadeOut(5000);

                $("#contact_name").val("");
                $("#contact_email").val("");
                $("#contact_number").val("");
                $("#contact_message").val("");
            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                //alert(xmlHttpRequest.responseText + ' - ' + textStatus + ' - ' + errorThrown);
                console.log(xmlHttpRequest.responseText);
                console.log(textStatus);
                console.log(errorThrown);

                $("#error").show();
                $("#error").fadeOut(5000);
            }
        });

    }
});




// --------------------------------------------------------------- CONTACT US MAPS
$(document).ready(function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        center: {
            lat: 12.9625811,
            lng: 77.6902677
        },
        zoom: 13
    });
    // Let's also add a marker while we're at it
    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(12.9625811, 77.6902677),
        map: map,
        icon: {
            url: '../assets/img/MapMarkerFinal.png',
        },
        animation: google.maps.Animation.BOUNCE
    });

    //var map2 = new google.maps.Map(document.getElementById('map2'), {
    //    center: {
    //        lat: 18.437462,
    //        lng: 79.448288
    //    },
    //    zoom: 13
    //});
    //// Let's also add a marker while we're at it
    //var marker = new google.maps.Marker({
    //    position: new google.maps.LatLng(18.437462, 79.448288),
    //    map: map,
    //    icon: {
    //        url: '../assets/img/MapMarkerFinal.png',
    //    },
    //    animation: google.maps.Animation.BOUNCE
    //});
});





// ------------------------------------------------------------------------ SAMPLE CODES


/*jQuery.validator.setDefaults({
    debug: true,
    success: "valid"
});

$(".registration-form-area").validate({
    rules: {
        r_password: "required",
        R_confirmpassword: {
            equalTo: "#r_password"
        }
    }
}); */
