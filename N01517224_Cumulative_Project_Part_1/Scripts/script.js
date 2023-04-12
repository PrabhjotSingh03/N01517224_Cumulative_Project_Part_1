window.onload = function () {

    //DOM for form elements
    var TeacherFname = document.getElementById("TeacherFname");
    var TeacherLname = document.getElementById("TeacherLname");
    var EmployeeNumber = document.getElementById("EmployeeNumber");
    var HireDate = document.getElementById("HireDate");
    var Salary = document.getElementById("Salary");
    var teacherbutton = document.getElementById("teacherbutton");
    let errorMessage = document.getElementById(".errorMessage");
    let errorMessage2 = document.getElementById(".errorMessage2");
    let errorMessage3 = document.getElementById(".errorMessage3");
    let errorMessage4 = document.getElementById(".errorMessage4");
    let errorMessage5 = document.getElementById(".errorMessage5");

    //When the submit button clicked
    teacherbutton.onclick = processForm;

    //Do form validation
    function processForm() {
        //Check whether or not each input is filled
        if (TeacherFname.value === '') {
            errorMessage.style.visibility = 'unset';
            TeacherFname.focus();
            return false;
        } else {
            errorMessage.style.visibility = 'hidden';
        }

        if (TeacherLname.value === '') {
            errorMessage2.style.visibility = 'unset';
            TeacherLname.focus();
            return false;
        } else {
            errorMessage2.style.visibility = 'hidden';
        }

        if (EmployeeNumber.value === '') {
            errorMessage3.style.visibility = 'unset';
            EmployeeNumber.focus();
            return false;
        } else {
            errorMessage3.style.visibility = 'hidden';
        }

        if (HireDate.value === '') {
            errorMessage4.style.visibility = 'unset';
            HireDate.focus();
            return false;
        } else {
            errorMessage4.style.visibility = 'hidden';
        }

        if (Salary.value === '') {
            errorMessage5.style.visibility = 'unset';
            Salary.focus();
            return false;
        } else {
            errorMessage5.style.visibility = 'hidden';
        }

        //If all fields filled return true
        return true;
    }
    return false;
}
