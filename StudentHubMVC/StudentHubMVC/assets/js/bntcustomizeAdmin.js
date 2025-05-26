
function nameToKeyword () {
    var Val = $("#Name").val();
    if (Val != null && Val != '') {
        Val = Val.replace(/[^a-z0-9\s]/gi, "");
        Val = Val.replace(/ /g, "-");
        Val = Val.replace(/_/g, "-");
        Val = Val.toLowerCase();
        $("#UrlKeyword").val(Val);
    }

    Val = $("#TypeValue").val();
    if (Val != null && Val != '') {
        Val = Val.replace(/[^a-z0-9\s]/gi, "");
        Val = Val.replace(/ /g, "-");
        Val = Val.replace(/_/g, "-");
        Val = Val.toLowerCase();
        $("#TypeValueUrlKeyword").val(Val);
    }
}

$(document).ready(function () {
    // assigning data to instructors dropdowns
    $("#Instructors1").val($("#InstructorId1").val());
    $("#Instructors2").val($("#InstructorId2").val());
    $("#Courses").val($("#CourseId").val());
});

$("#Courses").change(function () {
    $("#CourseId").val($("#Courses").val());
});

$("#Instructors1").change(function () {
    $("#InstructorId1").val($("#Instructors1").val());
});

$("#Instructors2").change(function () {
    $("#InstructorId2").val($("#Instructors2").val());
});

$(".message").fadeOut(5000);
$(".errormessage").fadeOut(5000);

$("#newquestion").click(function () {
    $("#addquestion").show();
});


// -------------------------------------------------------- EDIT TEST QUESTION
$(".editlbl").click(function () {
    var question = $(this).closest(".fullquestion");

    $("#addquestion").show();

    $('#Sno').val(question.find(".qid").html());
    $('#Question').val(question.find(".questionvalue").html());
    $('#Answer').val(question.find(".answervalue").html());
    $('#OptionA').val(question.find(".optionavalue").html());
    $('#OptionB').val(question.find(".optionbvalue").html());
    $('#OptionC').val(question.find(".optioncvalue").html());
    $('#OptionD').val(question.find(".optiondvalue").html());
    
    jQuery('html,body').animate({ scrollTop: 0 }, 500);
});


// ------------------------------------------------------- EDIT DROPDOWN TYPE
$(".edittype").click(function () {

    var edit = $(this).closest("tr");
    //alert(edit.find("td:eq(1)").text());
    $("#DropdownTypeId").val(edit.find("td:eq(0)").text());
    $("#GroupName").val(edit.find("td:eq(1)").text());
    $("#TypeValue").val(edit.find("td:eq(2)").text());
    $("#TypeValueUrlKeyword").val(edit.find("td:eq(3)").text());

    jQuery('html,body').animate({ scrollTop: 0 }, 500);
});


// ---------------------------------------------------------- MOCKTEST COURSE SELECT POSTBACK
$(".mocketestcoursepostback").change(function () {
    //alert($(this).val());
    var currentLoc = window.location.href;
    var mainURL = window.location.origin;

    window.location.href = mainURL + "/admin/savemocktest/" + $(this).val();
});


// ---------------------------------------------------------- QUESTIONPAPERS MANAGER COURSE SELECT POSTBACK
$(".questionpapercoursepostback").change(function () {
    //alert($(this).val());
    var currentLoc = window.location.href;
    var mainURL = window.location.origin;

    window.location.href = mainURL + "/admin/questionpapersmanager/" + $(this).val();
});


// ---------------------------------------------------------- TUTORIALS MANAGER SELECT POSTBACK
$("#TutorialCourses").change(function () {
    
    var currentLoc = window.location.href;
    var mainURL = window.location.origin;

    window.location.href = mainURL + "/admin/savetutorials/" + $(this).val();
});


// -------------------------------------------------------- DELETE CONFIRM POP-UP
$(".delete").click(function (e) {
    if (!confirm("Are You Sure?"))
        e.preventDefault();
});

//$(".deletesmall").click(function (e) {
//    if (!confirm("Are You Sure?"))
//        e.preventDefault();
//});

function deleteConfirm(msg) {
    if (!confirm(msg)) {
        return false;
    }
    else {
        return true;
    }
}


// --------------------------------------------------------- TEXT AREA HTML EDITOR
$('.texttoolbox').each(function () {
    var editor = new Jodit(this);
    //editor.value = '<p>start</p>';
});
$('.richtexteditor').each(function () {
    var editor = new Jodit(this);
    //editor.value = '<p>start</p>';
});


// ---------------------------------------------------------- JQUERY PAGINATION.JS
let rows = []
$('table tbody tr').each(function (i, row) {
    return rows.push(row);
});

//$('#pagination').pagination({
//    dataSource: rows,
//    pageSize: 1,
//    callback: function (data, pagination) {
//        $('tbody').html(data);
//    }
//})


// ------------------------------------------------------------ DATEPICKER
$(".datepicker").datepicker({
    dateFormat: 'dd-mm-yy'
});


// ------------------------------------------------------------- DESIGNER HIDING
if ($("#LoginEmail").val() != null && $("#LoginEmail").val() != "") {

    if ($("#LoginEmail").val() == "design@studenthub.in") {
        $("form input[type=submit]").hide();
    }
}