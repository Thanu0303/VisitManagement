/*
 * SmartWizard 3.3.1 plugin
 * jQuery Wizard control Plugin
 * by Dipu
 *
 * Refactored and extended:
 * https://github.com/mstratman/jQuery-Smart-Wizard
 *
 * Original URLs:
 * http://www.techlaboratory.net
 * http://tech-laboratory.blogspot.com
 */





var step1 = 0;
var step2 = 0;
var step3 = 0;
var step4 = 0;
var count = 0;
function nextbutton() {
    var currentstep = $("#wizard").smartWizard("currentStep");
    if (currentstep != 4) {
        document.getElementById('hidefinish').hidden = true;
        document.getElementById('hideNext').removeAttribute("hidden");
    }
};
function finalConfirmation() {
    step1 = 0; count = 0; step2 = 0; step3 = 0; step4 = 0;
    var currentstep = $("#wizard").smartWizard("currentStep");
    if (currentstep == 4) {
        document.getElementById('hidefinish').removeAttribute("hidden");
        document.getElementById('hideNext').hidden = true;
    }
    if (currentstep == 1) {
        var visitorName = document.getElementById("visitorName").value;
        document.getElementById("visitorNameConfirmation").value = visitorName;
        document.getElementById("visitorNameConfirmation").readOnly = true;

        var visitorEmail = document.getElementById("emailAddress").value;
        document.getElementById("emailConfirmation").value = visitorEmail;
        document.getElementById("emailConfirmation").readOnly = true;

        var visitor = document.getElementById("phone").value;
        document.getElementById("phoneConfirmation").value = visitor;
        document.getElementById("phoneConfirmation").readOnly = true;

        var visitor = document.getElementById("company").value;
        document.getElementById("companyConfirmation").value = visitor;
        document.getElementById("companyConfirmation").readOnly = true;

        var visitor = document.getElementById("designation").value;
        document.getElementById("designationConfirmation").value = visitor;
        document.getElementById("designationConfirmation").readOnly = true;


        var tabId = 1;
        if (tabId == 1) {
            debugger;
            if (document.getElementById("visitorName").value == "") {
                count += 1;
                $("#ValvisitorName").show();
                document.getElementById("visitorName").classList.add('parsley-error');
            } else {
                if (document.getElementById("visitorName").value != "") {
                    debugger;
                    var expr = /^\s*[a-zA-Z,\s]+\s*$/;
                    var name = document.getElementById("visitorName").value;
                    if (expr.test(name)) {

                        $("#nameValidate").hide();
                        document.getElementById("visitorName").classList.remove('parsley-error');

                        step1 += 1;
                    }
                    else {
                        count += 1;
                        $("#nameValidate").show();
                        document.getElementById("visitorName").classList.add('parsley-error');
                    }

                }
                $("#ValvisitorName").hide();
                document.getElementById("visitorName").classList.remove('parsley-error');
                step1 += 1;              
            }


            if (document.getElementById("emailAddress").value == "") {
                count += 1;
                $("#ValemailAddress").show();
                document.getElementById("emailAddress").classList.add('parsley-error');
            }
            else {
                if (document.getElementById("emailAddress").value != "") {
                    debugger;
                    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
                    var email = document.getElementById("emailAddress").value;
                    if (expr.test(email)) {

                        $("#emailValidate").hide();
                        document.getElementById("emailAddress").classList.remove('parsley-error');

                        step1 += 1;
                    }
                    else {
                        count += 1;
                        $("#emailValidate").show();
                        document.getElementById("emailAddress").classList.add('parsley-error');
                    }

                }
                $("#ValemailAddress").hide();
                document.getElementById("emailAddress").classList.remove('parsley-error');
                step1 += 1;                
            }




            if (document.getElementById("phone").value == "") {
                $("#Valphone").show();
                document.getElementById("phone").classList.add('parsley-error');
                //isValid = false;
                count += 1;

            }

            else {
                if (document.getElementById("phone").value != "") {
                    debugger;
                    var expr = /^\d{10}$/;
                    var phonenum = document.getElementById("phone").value;
                    if (expr.test(phonenum)) {

                        $("#phoneValidate").hide();
                        document.getElementById("phone").classList.remove('parsley-error');

                        step1 += 1;
                    }
                    else {
                        count += 1;
                        $("#phoneValidate").show();
                        document.getElementById("phone").classList.add('parsley-error');
                    }
                }
                $("#Valphone").hide();
                document.getElementById("phone").classList.remove('parsley-error');
                step1 += 1;
            }
            if (document.getElementById("company").value == "") {
                $("#Valcompany").show();
                document.getElementById("company").classList.add('parsley-error');
                count += 1;

            } else {
                $("#Valcompany").hide();
                document.getElementById("company").classList.remove('parsley-error');
                step1 += 1;
            }
            if (document.getElementById("designation").value == "") {
                $("#Valdesignation").show();
                document.getElementById("designation").classList.add('parsley-error');
                count += 1;

            } else {

                $("#Valdesignation").hide();
                document.getElementById("designation").classList.remove('parsley-error');
                step1 += 1;
            }
        }

    }
    else if (currentstep == 2) {

        //var tabId = 2;
        var visitor = document.getElementById("visitingDate").value;
        document.getElementById("dateConfirmation").value = visitor;
        document.getElementById("dateConfirmation").readOnly = true;


        var visitor = document.getElementById("startTime").value;
        document.getElementById("startTimeConfirmation").value = visitor;
        document.getElementById("startTimeConfirmation").readOnly = true;


        var visitor = document.getElementById("endTime").value;
        document.getElementById("endTimeConfirmation").value = visitor;

        document.getElementById("endTimeConfirmation").readOnly = true;
        var tabId = 2;
        if (tabId == 2) {
            debugger;
            if (document.getElementById("visitingDate").value == "") {
                $("#ValvisitingDate").show();
                document.getElementById("visitingDate").classList.add('parsley-error');
                //isValid = false;
                count += 1;
            }

            else {
                var Startdate = /*moment(*/document.getElementById("visitingDate").value/*).format('DD-MM-YYYY')*/
                Startdate = new Date(Startdate).toLocaleDateString();
                // var date = new Date().getDate() + ":" + new Date().getMonth() + ":" + new Date().getFullYear();
                var visitingDate =/* moment(*/new Date().toLocaleDateString()/*).format('DD-MM-YYYY')*/;
                visitingDate = new Date(visitingDate).toLocaleDateString();
                if (document.getElementById("visitingDate").value != "") {
                    if ((Startdate >= visitingDate)) {

                        $("#dateValidate").hide();
                        document.getElementById("visitingDate").classList.remove('parsley-error');

                        step2 += 1;
                    }
                    else {
                        count += 1;
                        $("#dateValidate").show();
                        document.getElementById("visitingDate").classList.add('parsley-error');

                    }
                }
            $("#ValvisitingDate").hide();
            document.getElementById("visitingDate").classList.remove('parsley-error');
            step2 += 1;
        }
        if (document.getElementById("startTime").value == "00:00:00.000" || document.getElementById("startTime").value == "") {
            $("#ValstartTime").show();
            document.getElementById("startTime").classList.add('parsley-error');
            //isValid = false;
            count += 1;
        } else {
            $("#ValstartTime").hide();
            document.getElementById("startTime").classList.remove('parsley-error');
            //isValid = true;
            step2 += 1;
            //tabId = tabId + 1;
        }

        if (document.getElementById("endTime").value == "00:00:00.000" || document.getElementById("endTime").value == "") {
            $("#ValendTime").show();
            document.getElementById("endTime").classList.add('parsley-error');
            //isValid = false;
            count += 1;
        } else {
            $("#ValendTime").hide();
            document.getElementById("endTime").classList.remove('parsley-error');
            //isValid = true;
            step2 += 1;
            //tabId = tabId + 1;
        }
        //isValid == true;
        //return tabId +2 ;  
    }

}


var id = document.getElementById("ddlprimaryContact");
var selectedPrimaryRes = id.options[id.selectedIndex].text;
document.getElementById("primaryContactConfirmation").value = selectedPrimaryRes;
document.getElementById("primaryContactConfirmation").readOnly = true;

document.getElementById("secondaryContactConfirmation").value = $('#ddlsecondaryContact option:selected').toArray().map(item => item.text).join();
document.getElementById("secondaryContactConfirmation").readOnly = true;
var second = [];
var values = $('#ddlsecondaryContact option:selected').toArray().map(item => item.value);
for (var i = 0; values.length > i; i++) {
    second.push(parseInt(values[i]));
    }
    document.getElementById("SecondaryValues").value = second;

    //var values = $('#ddlprimaryContact option:selected').toArray.map(item => item.value);
    //for (var i = 0; values.length > i; i++) {
    //    first.push(parseInt(values[i]));
    //}
    //document.getElementById("PrimaryValues").value = first;



    //else if (currentstep == 3) {
    //    var id = document.getElementById("selUser");
    //    var selectedPrimaryRes = id.options[id.selectedIndex].text;
    //    document.getElementById("primaryContactConfirmation").value = selectedPrimaryRes;
    //    document.getElementById("primaryContactConfirmation").readOnly = true;

    //    document.getElementById("secondaryContactConfirmation").value = $('#ddlsecondaryContact option:selected').toArray().map(item => item.text).join();
    //    document.getElementById("secondaryContactConfirmation").readOnly = true;

    //    //var id = document.getElementById("ddlprimaryContact");
    //    //var selectedPrimaryRes = id.options[id.selectedIndex].text;
    //    //document.getElementById("primaryContactConfirmation").value = selectedPrimaryRes;
    //    //document.getElementById("primaryContactConfirmation").readOnly = true;

    //    //     document.getElementById("secondaryContactConfirmation").value = $('#ddlsecondaryContact option:selected').toArray().map(item => item.text).join();
    //    //// alert(document.getElementById("secondaryContactConfirmation").value);
    //    // document.getElementById("secondaryContactConfirmation").readOnly = true;
    //    // debugger;
    //    var second = [];
    //    var values = $('#ddlsecondaryContact option:selected').toArray().map(item => item.value);
    //    for (var i = 0; values.length > i; i++) {
    //        second.push(parseInt(values[i]));
    //    }
    //    document.getElementById("SecondaryValues").value = second;

    //    var tabId = 2;
    //    if (tabId == 2) {
    //        if (document.getElementById("selUser").value == "") {
    //            $("#ValprimaryContact").show();
    //            document.getElementById("selUser").classList.add('parsley-error');
    //            //isValid = false;
    //            count += 1;
    //        } else {
    //            $("#ValprimaryContact").hide();
    //            document.getElementById("selUser").classList.remove('parsley-error');
    //            //isValid = true;
    //            step2 += 1;
    //            //tabId = tabId + 1;
    //        }
    //        if (document.getElementById("ddlsecondaryContact").value == "") {
    //            $("#ValsecondaryContact").show();
    //            document.getElementById("ddlsecondaryContact").classList.add('parsley-error');
    //            //isValid = false;
    //            count += 1;
    //        } else {
    //            $("#ValsecondaryContact").hide();
    //            document.getElementById("ddlsecondaryContact").classList.remove('parsley-error');
    //            //isValid = true;
    //            step2 += 1;
    //            //tabId = tabId + 1;
    //        }

    //    }
    //    //else if (currentstep == 3) {
    //    //    var id = document.getElementById("ddlprimaryContact");
    //    //    var selectedPrimaryRes = id.options[id.selectedIndex].text;
    //    //    document.getElementById("primaryContactConfirmation").value = selectedPrimaryRes;
    //    //    document.getElementById("primaryContactConfirmation").readOnly = true;

    //    //    document.getElementById("secondaryContactConfirmation").value = $('#ddlsecondaryContact option:selected').toArray().map(item => item.text).join();
    //    //    document.getElementById("secondaryContactConfirmation").readOnly = true;

    //    //    if (document.getElementById("ddlprimaryContact").value == "") {
    //    //        $("#ValddlprimaryContact").show();
    //    //        document.getElementById("ddlprimaryContact").classList.add('parsley-error');
    //    //        isValid = false;
    //    //    } else {
    //    //        $("#ValddlprimaryContact").hide();
    //    //        document.getElementById("ddlprimaryContact").classList.remove('parsley-error');
    //    //        isValid = true;
    //    //        //tabId = tabId + 1;
    //    //    }
    //    //    if (document.getElementById("ddlsecondaryContact").value == "") {
    //    //        $("#ValddlsecondaryContact").show();
    //    //        document.getElementById("ddlsecondaryContact").classList.add('parsley-error');
    //    //        isValid = false;
    //    //    } else {
    //    //        $("#ValddlsecondaryContact").hide();
    //    //        document.getElementById("ddlsecondaryContact").classList.remove('parsley-error');
    //    //        isValid = true;
    //    //        //tabId = tabId + 1;
    //    //    }
    //    //}
    //}
}









//var isValid = false;
//function finalConfirmation() {
//    var currentstep = $("#wizard").smartWizard("currentStep");

//    if (currentstep == 1) {
//        var visitorName = document.getElementById("visitorName").value;
//        document.getElementById("visitorNameConfirmation").value = visitorName;
//        document.getElementById("visitorNameConfirmation").readOnly = true;

//        var visitorEmail = document.getElementById("emailAddress").value;
//        document.getElementById("emailConfirmation").value = visitorEmail;
//        document.getElementById("emailConfirmation").readOnly = true;

//        var visitor = document.getElementById("phone").value;
//        document.getElementById("phoneConfirmation").value = visitor;
//        document.getElementById("phoneConfirmation").readOnly = true;

//        var visitor = document.getElementById("company").value;
//        document.getElementById("companyConfirmation").value = visitor;
//        document.getElementById("companyConfirmation").readOnly = true;

//        var visitor = document.getElementById("designation").value;
//        document.getElementById("designationConfirmation").value = visitor;
//        document.getElementById("designationConfirmation").readOnly = true;

//        var tabId = 1;
//        if (tabId == 1) {
//            debugger;
//            //function tab1() {
//            if (document.getElementById("visitorName").value == "") {
//                $("#ValvisitorName").show();
//                document.getElementById("visitorName").classList.add('parsley-error');
//                isValid = false;
//            } else {
//                $("#ValvisitorName").hide();
//                document.getElementById("visitorName").classList.remove('parsley-error');
//                isValid = true;
//                //tabId = tabId + 1;
//            }

//            if (document.getElementById("emailAddress").value == "") {
//                $("#ValemailAddress").show();
//                document.getElementById("emailAddress").classList.add('parsley-error');
//                isValid = false;
//            } else {
//                $("#ValemailAddress").hide();
//                document.getElementById("emailAddress").classList.remove('parsley-error');
//                isValid = true;
//                //tabId = tabId + 1;
//            }

//            if (document.getElementById("phone").value == "") {
//                $("#Valphone").show();
//                document.getElementById("phone").classList.add('parsley-error');
//                isValid = false;
//            } else {
//                $("#Valphone").hide();
//                document.getElementById("phone").classList.remove('parsley-error');
//                isValid = true;
//                //tabId = tabId + 1;
//            }
//            if (document.getElementById("company").value == "") {
//                $("#Valcompany").show();
//                document.getElementById("company").classList.add('parsley-error');
//                isValid = false;
//            } else {
//                $("#Valcompany").hide();
//                document.getElementById("company").classList.remove('parsley-error');
//                isValid = true;
//                //tabId = tabId + 1;
//            }
//            if (document.getElementById("designation").value == "") {
//                $("#Valdesignation").show();
//                document.getElementById("designation").classList.add('parsley-error');
//                isValid = false;
//            } else {
//                $("#Valdesignation").hide();
//                document.getElementById("designation").classList.remove('parsley-error');
//                isValid = true;
//                //tabId = tabId + 1;
//            }
//            //isValid == true;
//            //return tabId + 1;
//            //}
//            //tabId = tabId + 1;  
//        }

//    }
//    else if (currentstep == 2) {

//        //var tabId = 2;
//        var visitor = document.getElementById("visitingDate").value;
//        document.getElementById("dateConfirmation").value = visitor;
//        document.getElementById("dateConfirmation").readOnly = true;


//        var visitor = document.getElementById("startTime").value;
//        document.getElementById("startTimeConfirmation").value = visitor;
//        document.getElementById("startTimeConfirmation").readOnly = true;


//        var visitor = document.getElementById("endTime").value;
//        document.getElementById("endTimeConfirmation").value = visitor;

//        document.getElementById("endTimeConfirmation").readOnly = true;
//        var tabId = 2;
//        if (tabId == 2) {
//            debugger;
//            if (document.getElementById("visitingDate").value == "") {
//                $("#ValvisitingDate").show();
//                document.getElementById("visitingDate").classList.add('parsley-error');
//                isValid = false;
//            } else {
//                $("#ValvisitingDate").hide();
//                document.getElementById("visitingDate").classList.remove('parsley-error');
//                isValid = true;
//                //tabId = tabId + 1;
//            }

//            if (document.getElementById("startTime").value == "00:00:00.000" || document.getElementById("startTime").value == "") {
//                $("#ValstartTime").show();
//                document.getElementById("startTime").classList.add('parsley-error');
//                isValid = false;
//            } else {
//                $("#ValstartTime").hide();
//                document.getElementById("startTime").classList.remove('parsley-error');
//                isValid = true;
//                //tabId = tabId + 1;
//            }

//            if (document.getElementById("endTime").value == "00:00:00.000" || document.getElementById("endTime").value == "") {
//                $("#ValendTime").show();
//                document.getElementById("endTime").classList.add('parsley-error');
//                isValid = false;
//            } else {
//                $("#ValendTime").hide();
//                document.getElementById("endTime").classList.remove('parsley-error');
//                isValid = true;
//                //tabId = tabId + 1;
//            }
//            //isValid == true;
//            //return tabId +2 ;  
//        }

//    }

//           var id = document.getElementById("ddlprimaryContact");
//        var selectedPrimaryRes = id.options[id.selectedIndex].text;
//        document.getElementById("primaryContactConfirmation").value = selectedPrimaryRes;
//        document.getElementById("primaryContactConfirmation").readOnly = true;

//        document.getElementById("secondaryContactConfirmation").value = $('#ddlsecondaryContact option:selected').toArray().map(item => item.text).join();
//        document.getElementById("secondaryContactConfirmation").readOnly = true;

//        var id = document.getElementById("ddlprimaryContact");
//        var selectedPrimaryRes = id.options[id.selectedIndex].text;
//        document.getElementById("primaryContactConfirmation").value = selectedPrimaryRes;
//        document.getElementById("primaryContactConfirmation").readOnly = true;

//   //     document.getElementById("secondaryContactConfirmation").value = $('#ddlsecondaryContact option:selected').toArray().map(item => item.text).join();
//   //// alert(document.getElementById("secondaryContactConfirmation").value);
//   // document.getElementById("secondaryContactConfirmation").readOnly = true;
//   // debugger;
//    var second = [];
//    var values = $('#ddlsecondaryContact option:selected').toArray().map(item => item.value);
//    for (var i = 0; values.length > i; i++) {
//        second.push(parseInt(values[i]));
//    }
//    document.getElementById("SecondaryValues").value = second;

//    //else if (currentstep == 3) {
//    //    var id = document.getElementById("ddlprimaryContact");
//    //    var selectedPrimaryRes = id.options[id.selectedIndex].text;
//    //    document.getElementById("primaryContactConfirmation").value = selectedPrimaryRes;
//    //    document.getElementById("primaryContactConfirmation").readOnly = true;

//    //    document.getElementById("secondaryContactConfirmation").value = $('#ddlsecondaryContact option:selected').toArray().map(item => item.text).join();
//    //    document.getElementById("secondaryContactConfirmation").readOnly = true;

//    //    if (document.getElementById("ddlprimaryContact").value == "") {
//    //        $("#ValddlprimaryContact").show();
//    //        document.getElementById("ddlprimaryContact").classList.add('parsley-error');
//    //        isValid = false;
//    //    } else {
//    //        $("#ValddlprimaryContact").hide();
//    //        document.getElementById("ddlprimaryContact").classList.remove('parsley-error');
//    //        isValid = true;
//    //        //tabId = tabId + 1;
//    //    }
//    //    if (document.getElementById("ddlsecondaryContact").value == "") {
//    //        $("#ValddlsecondaryContact").show();
//    //        document.getElementById("ddlsecondaryContact").classList.add('parsley-error');
//    //        isValid = false;
//    //    } else {
//    //        $("#ValddlsecondaryContact").hide();
//    //        document.getElementById("ddlsecondaryContact").classList.remove('parsley-error');
//    //        isValid = true;
//    //        //tabId = tabId + 1;
//    //    }
//    //}
//}

//function finalConfirmation() {

//    var isValid = true;
//    //debugger;
//    if (document.getElementById("visitorName").value == "") {
//        document.getElementById("visitorName").classList.add('parsley-error');
//        document.getElementById("vn").innerHTML = 'this feild is required';

//        //SmartWizard('#step-1', 1);
//        //SmartWizard.prototype.goBackward;
//        //isValid =false;
//        //window.location.replace('https://localhost:44389/Visitor/Create#step-1')
//    }
//    else {
//        document.getElementById("visitorName").classList.remove('parsley-error');
//        document.getElementById("vn").innerHTML = '';
//    }
//    var visitor = document.getElementById("visitorName").value;
//    document.getElementById("visitorNameConfirmation").value = visitor;
//    document.getElementById("visitorNameConfirmation").readOnly = true;

//    //if (document.getElementById("emailAddress").value == "") {
//    //    document.getElementById("emailAddress").classList.add('parsley-error');
//    //    document.getElementById("em").innerHTML = 'this feild is required';
//    //    //SmartWizard('#step-1', 1);
//    //    SmartWizard.prototype.goBackward;
//    //    isValid = false;
//    //    window.location.replace('https://localhost:44389/Visitor/Create#step-1')
//    //}
//    //else {
//    //    document.getElementById("emailAddress").classList.remove('parsley-error');
//    //    document.getElementById("em").innerHTML = '';
//    //}
//    var visitor = document.getElementById("emailAddress").value;
//    document.getElementById("emailConfirmation").value = visitor;
//    document.getElementById("emailConfirmation").readOnly = true;


//    //if (document.getElementById("phone").value == "") {
//    //    document.getElementById("phone").classList.add('parsley-error');
//    //    document.getElementById("pn").innerHTML = 'this feild is required';
//    //    //SmartWizard('#step-1', 1);
//    //    SmartWizard.prototype.goBackward;
//    //    isValid = false;
//    //    window.location.replace('https://localhost:44389/Visitor/Create#step-1')
//    //}
//    //else {
//    //    document.getElementById("phone").classList.remove('parsley-error');
//    //    document.getElementById("pn").innerHTML = '';
//    //}
//    var visitor = document.getElementById("phone").value;
//    document.getElementById("phoneConfirmation").value = visitor;
//    document.getElementById("phoneConfirmation").readOnly = true;


//    //if (document.getElementById("company").value == "") {
//    //    document.getElementById("company").classList.add('parsley-error');
//    //    document.getElementById("cn").innerHTML = 'this feild is required';
//    //    //SmartWizard('#step-1', 1);
//    //    SmartWizard.prototype.goBackward;
//    //    isValid = false;
//    //    window.location.replace('https://localhost:44389/Visitor/Create#step-1')
//    //}
//    //else {
//    //    document.getElementById("company").classList.remove('parsley-error');
//    //    document.getElementById("cn").innerHTML = '';
//    //}
//    var visitor = document.getElementById("company").value;
//    document.getElementById("companyConfirmation").value = visitor;
//    document.getElementById("companyConfirmation").readOnly = true;

//    //if (document.getElementById("designation").value == "") {
//    //    document.getElementById("designation").classList.add('parsley-error');
//    //    document.getElementById("dn").innerHTML = 'this feild is required';
//    //    //SmartWizard('#step-1', 1);
//    //    SmartWizard.prototype.goBackward;
//    //    isValid = false;
//    //    window.location.replace('https://localhost:44389/Visitor/Create#step-1')
//    //}
//    //else {
//    //    document.getElementById("designation").classList.remove('parsley-error');
//    //    document.getElementById("dn").innerHTML = '';
//    //}
//    var visitor = document.getElementById("designation").value;
//    document.getElementById("designationConfirmation").value = visitor;
//    document.getElementById("designationConfirmation").readOnly = true;

//    //return false;

//    //if (document.getElementById("visitingDate").value == "") {
//    //    document.getElementById("visitingDate").classList.add('parsley-error');
//    //    document.getElementById("vd").innerHTML = 'this feild is required';
//    //    //SmartWizard('#step-1', 1);
//    //    SmartWizard.prototype.goBackward;
//    //    window.location.replace('https://localhost:44389/Visitor/Create#step-1')
//    //}
//    //else {
//    //    document.getElementById("visitingDate").classList.remove('parsley-error');
//    //    document.getElementById("vd").innerHTML = '';
//    //}
//    var visitor = document.getElementById("visitingDate").value;
//    document.getElementById("dateConfirmation").value = visitor;
//    document.getElementById("dateConfirmation").readOnly = true;

//    //if (document.getElementById("startTime").value == "") {
//    //    document.getElementById("startTime").classList.add('parsley-error');
//    //    document.getElementById("st").innerHTML = 'this feild is required';
//    //    //SmartWizard('#step-1', 1);
//    //    SmartWizard.prototype.goBackward;
//    //    window.location.replace('https://localhost:44389/Visitor/Create#step-1')
//    //}
//    //else {
//    //    document.getElementById("startTime").classList.remove('parsley-error');
//    //    document.getElementById("st").innerHTML = '';
//    //}
//    var visitor = document.getElementById("startTime").value;
//    document.getElementById("startTimeConfirmation").value = visitor;
//    document.getElementById("startTimeConfirmation").readOnly = true;

//    //if (document.getElementById("endTime").value == "") {
//    //    document.getElementById("endTime").classList.add('parsley-error');
//    //    document.getElementById("et").innerHTML = 'this feild is required';
//    //    //SmartWizard('#step-1', 1);
//    //    SmartWizard.prototype.goBackward;
//    //    window.location.replace('https://localhost:44389/Visitor/Create#step-1')
//    //}
//    //else {
//    //    document.getElementById("endTime").classList.remove('parsley-error');
//    //    document.getElementById("et").innerHTML = '';
//    //}
//    var visitor = document.getElementById("endTime").value;
//    document.getElementById("endTimeConfirmation").value = visitor;
//    document.getElementById("endTimeConfirmation").readOnly = true;

//    var id = document.getElementById("ddlprimaryContact");
//    var selectedPrimaryRes = id.options[id.selectedIndex].text;
//    document.getElementById("primaryContactConfirmation").value = selectedPrimaryRes;
//    document.getElementById("primaryContactConfirmation").readOnly = true;

//    document.getElementById("secondaryContactConfirmation").value = $('#ddlsecondaryContact option:selected').toArray().map(item => item.text).join();
//    document.getElementById("secondaryContactConfirmation").readOnly = true;
//}


function SmartWizard(target, options) {
    this.target = target;
    this.options = options;
    this.curStepIdx = options.selected;
    this.steps = $(target).children("ul").children("li").children("a"); // Get all anchors
    this.contentWidth = 0;
    this.msgBox = $('<div class="msgBox"><div class="content"></div><a href="#" class="close">X</a></div>');
    this.elmStepContainer = $('<div></div>').addClass("stepContainer");
    this.loader = $('<div>Loading</div>').addClass("loader");
    this.buttons = {
        next: $('<a id="hideNext">' + options.labelNext + '</a>').attr("onclick", "return finalConfirmation()").addClass("buttonNext"),
        finish: $('<button type="submit" id="hidefinish" hidden>' + options.labelFinish + '</button>').attr("href", "#").addClass("buttonFinish btn btn-success"),
        previous: $('<a onclick="nextbutton()">' + options.labelPrevious + '</a>').attr("href", "#").addClass("buttonPrevious")

    };

    /*
     * Private functions
     */

    var _init = function ($this) {
        var elmActionBar = $('<div></div>').addClass("actionBar");
        elmActionBar.append($this.msgBox);
        $('.close', $this.msgBox).click(function () {
            $this.msgBox.fadeOut("normal");
            return false;
        });

        var allDivs = $this.target.children('div');
        $this.target.children('ul').addClass("anchor");
        allDivs.addClass("content");

        // highlight steps with errors
        if ($this.options.errorSteps && $this.options.errorSteps.length > 0) {
            $.each($this.options.errorSteps, function (i, n) {
                $this.setError({ stepnum: n, iserror: true });
            });
        }

        $this.elmStepContainer.append(allDivs);
        elmActionBar.append($this.loader);
        $this.target.append($this.elmStepContainer);
        elmActionBar.append($this.buttons.finish)
            .append($this.buttons.next)
            .append($this.buttons.previous);
        $this.target.append(elmActionBar);
        this.contentWidth = $this.elmStepContainer.width();




        //if (isValid == true) {
        //    debugger;
        $($this.buttons.next).click(function () {
            debugger;
            //if (isValid == true) {
            if (count == 0 && (step1 == 5 | step2 == 3 | step3 == 0)) {

                debugger;
                $this.goForward();
                return false;
            }
        });
        //}



        //$($this.buttons.next).click(function() {
        //    $this.goForward();
        //    return false;
        //});


        $($this.buttons.previous).click(function () {
            $this.goBackward();
            return false;
        });
        $($this.buttons.finish).click(function () {
            if (!$(this).hasClass('buttonDisabled')) {
                if ($.isFunction($this.options.onFinish)) {
                    var context = { fromStep: $this.curStepIdx + 1 };
                    if (!$this.options.onFinish.call(this, $($this.steps), context)) {
                        return false;
                    }
                } else {
                    var frm = $this.target.parents('form');
                    if (frm && frm.length) {
                        frm.submit();
                    }
                }
            }
            return false;
        });

        $($this.steps).bind("click", function (e) {
            if ($this.steps.index(this) == $this.curStepIdx) {
                return false;
            }
            var nextStepIdx = $this.steps.index(this);
            var isDone = $this.steps.eq(nextStepIdx).attr("isDone") - 0;
            if (isDone == 1) {
                _loadContent($this, nextStepIdx);
            }
            return false;
        });

        // Enable keyboard navigation
        if ($this.options.keyNavigation) {
            $(document).keyup(function (e) {
                if (e.which == 39) { // Right Arrow
                    $this.goForward();
                } else if (e.which == 37) { // Left Arrow
                    $this.goBackward();
                }
            });
        }
        //  Prepare the steps
        _prepareSteps($this);
        // Show the first slected step
        _loadContent($this, $this.curStepIdx);
    };

    var _prepareSteps = function ($this) {
        if (!$this.options.enableAllSteps) {
            $($this.steps, $this.target).removeClass("selected").removeClass("done").addClass("disabled");
            $($this.steps, $this.target).attr("isDone", 0);
        } else {
            $($this.steps, $this.target).removeClass("selected").removeClass("disabled").addClass("done");
            $($this.steps, $this.target).attr("isDone", 1);
        }

        $($this.steps, $this.target).each(function (i) {
            $($(this).attr("href").replace(/^.+#/, '#'), $this.target).hide();
            $(this).attr("rel", i + 1);
        });
    };

    var _step = function ($this, selStep) {
        return $(
            $(selStep, $this.target).attr("href").replace(/^.+#/, '#'),
            $this.target
        );
    };

    var _loadContent = function ($this, stepIdx) {
        var selStep = $this.steps.eq(stepIdx);
        var ajaxurl = $this.options.contentURL;
        var ajaxurl_data = $this.options.contentURLData;
        var hasContent = selStep.data('hasContent');
        var stepNum = stepIdx + 1;
        if (ajaxurl && ajaxurl.length > 0) {
            if ($this.options.contentCache && hasContent) {
                _showStep($this, stepIdx);
            } else {
                var ajax_args = {
                    url: ajaxurl,
                    type: "POST",
                    data: ({ step_number: stepNum }),
                    dataType: "text",
                    beforeSend: function () {
                        $this.loader.show();
                    },
                    error: function () {
                        $this.loader.hide();
                    },
                    success: function (res) {
                        $this.loader.hide();
                        if (res && res.length > 0) {
                            selStep.data('hasContent', true);
                            _step($this, selStep).html(res);
                            _showStep($this, stepIdx);
                        }
                    }
                };
                if (ajaxurl_data) {
                    ajax_args = $.extend(ajax_args, ajaxurl_data(stepNum));
                }
                $.ajax(ajax_args);
            }
        } else {
            _showStep($this, stepIdx);
        }
    };

    var _showStep = function ($this, stepIdx) {
        var selStep = $this.steps.eq(stepIdx);
        var curStep = $this.steps.eq($this.curStepIdx);
        if (stepIdx != $this.curStepIdx) {
            if ($.isFunction($this.options.onLeaveStep)) {
                var context = { fromStep: $this.curStepIdx + 1, toStep: stepIdx + 1 };
                if (!$this.options.onLeaveStep.call($this, $(curStep), context)) {
                    return false;
                }
            }
        }
        $this.elmStepContainer.height(_step($this, selStep).outerHeight());
        var prevCurStepIdx = $this.curStepIdx;
        $this.curStepIdx = stepIdx;
        if ($this.options.transitionEffect == 'slide') {
            _step($this, curStep).slideUp("fast", function (e) {
                _step($this, selStep).slideDown("fast");
                _setupStep($this, curStep, selStep);
            });
        } else if ($this.options.transitionEffect == 'fade') {
            _step($this, curStep).fadeOut("fast", function (e) {
                _step($this, selStep).fadeIn("fast");
                _setupStep($this, curStep, selStep);
            });
        } else if ($this.options.transitionEffect == 'slideleft') {
            var nextElmLeft = 0;
            var nextElmLeft1 = null;
            var nextElmLeft = null;
            var curElementLeft = 0;
            if (stepIdx > prevCurStepIdx) {
                nextElmLeft1 = $this.contentWidth + 10;
                nextElmLeft2 = 0;
                curElementLeft = 0 - _step($this, curStep).outerWidth();
            } else {
                nextElmLeft1 = 0 - _step($this, selStep).outerWidth() + 20;
                nextElmLeft2 = 0;
                curElementLeft = 10 + _step($this, curStep).outerWidth();
            }
            if (stepIdx == prevCurStepIdx) {
                nextElmLeft1 = $($(selStep, $this.target).attr("href"), $this.target).outerWidth() + 20;
                nextElmLeft2 = 0;
                curElementLeft = 0 - $($(curStep, $this.target).attr("href"), $this.target).outerWidth();
            } else {
                $($(curStep, $this.target).attr("href"), $this.target).animate({ left: curElementLeft }, "fast", function (e) {
                    $($(curStep, $this.target).attr("href"), $this.target).hide();
                });
            }

            _step($this, selStep).css("left", nextElmLeft1).show().animate({ left: nextElmLeft2 }, "fast", function (e) {
                _setupStep($this, curStep, selStep);
            });
        } else {
            _step($this, curStep).hide();
            _step($this, selStep).show();
            _setupStep($this, curStep, selStep);
        }
        return true;
    };

    var _setupStep = function ($this, curStep, selStep) {
        $(curStep, $this.target).removeClass("selected");
        $(curStep, $this.target).addClass("done");

        $(selStep, $this.target).removeClass("disabled");
        $(selStep, $this.target).removeClass("done");
        $(selStep, $this.target).addClass("selected");

        $(selStep, $this.target).attr("isDone", 1);

        _adjustButton($this);

        if ($.isFunction($this.options.onShowStep)) {
            var context = { fromStep: parseInt($(curStep).attr('rel')), toStep: parseInt($(selStep).attr('rel')) };
            if (!$this.options.onShowStep.call(this, $(selStep), context)) {
                return false;
            }
        }
        if ($this.options.noForwardJumping) {
            // +2 == +1 (for index to step num) +1 (for next step)
            for (var i = $this.curStepIdx + 2; i <= $this.steps.length; i++) {
                $this.disableStep(i);
            }
        }
    };

    var _adjustButton = function ($this) {
        if (!$this.options.cycleSteps) {
            if (0 >= $this.curStepIdx) {
                $($this.buttons.previous).addClass("buttonDisabled");
                if ($this.options.hideButtonsOnDisabled) {
                    $($this.buttons.previous).hide();
                }
            } else {
                $($this.buttons.previous).removeClass("buttonDisabled");
                if ($this.options.hideButtonsOnDisabled) {
                    $($this.buttons.previous).show();
                }
            }
            if (($this.steps.length - 1) <= $this.curStepIdx) {
                $($this.buttons.next).addClass("buttonDisabled");
                if ($this.options.hideButtonsOnDisabled) {
                    $($this.buttons.next).hide();
                }
            } else {
                $($this.buttons.next).removeClass("buttonDisabled");
                if ($this.options.hideButtonsOnDisabled) {
                    $($this.buttons.next).show();
                }
            }
        }
        // Finish Button
        if (!$this.steps.hasClass('disabled') || $this.options.enableFinishButton) {
            $($this.buttons.finish).removeClass("buttonDisabled");
            if ($this.options.hideButtonsOnDisabled) {
                $($this.buttons.finish).show();
            }
        } else {
            $($this.buttons.finish).addClass("buttonDisabled");
            if ($this.options.hideButtonsOnDisabled) {
                $($this.buttons.finish).hide();
            }
        }
    };

    /*
     * Public methods
     */

    SmartWizard.prototype.goForward = function () {
        var nextStepIdx = this.curStepIdx + 1;
        if (this.steps.length <= nextStepIdx) {
            if (!this.options.cycleSteps) {
                return false;
            }
            nextStepIdx = 0;
        }
        _loadContent(this, nextStepIdx);
    };

    SmartWizard.prototype.goBackward = function () {
        var nextStepIdx = this.curStepIdx - 1;
        if (0 > nextStepIdx) {
            if (!this.options.cycleSteps) {
                return false;
            }
            nextStepIdx = this.steps.length - 1;
        }
        _loadContent(this, nextStepIdx);
    };

    SmartWizard.prototype.goToStep = function (stepNum) {
        var stepIdx = stepNum - 1;
        if (stepIdx >= 0 && stepIdx < this.steps.length) {
            _loadContent(this, stepIdx);
        }
    };
    SmartWizard.prototype.enableStep = function (stepNum) {
        var stepIdx = stepNum - 1;
        if (stepIdx == this.curStepIdx || stepIdx < 0 || stepIdx >= this.steps.length) {
            return false;
        }
        var step = this.steps.eq(stepIdx);
        $(step, this.target).attr("isDone", 1);
        $(step, this.target).removeClass("disabled").removeClass("selected").addClass("done");
    }
    SmartWizard.prototype.disableStep = function (stepNum) {
        var stepIdx = stepNum - 1;
        if (stepIdx == this.curStepIdx || stepIdx < 0 || stepIdx >= this.steps.length) {
            return false;
        }
        var step = this.steps.eq(stepIdx);
        $(step, this.target).attr("isDone", 0);
        $(step, this.target).removeClass("done").removeClass("selected").addClass("disabled");
    }
    SmartWizard.prototype.currentStep = function () {
        return this.curStepIdx + 1;
    }

    SmartWizard.prototype.showMessage = function (msg) {
        $('.content', this.msgBox).html(msg);
        this.msgBox.show();
    }
    SmartWizard.prototype.hideMessage = function () {
        this.msgBox.fadeOut("normal");
    }
    SmartWizard.prototype.showError = function (stepnum) {
        this.setError(stepnum, true);
    }
    SmartWizard.prototype.hideError = function (stepnum) {
        this.setError(stepnum, false);
    }
    SmartWizard.prototype.setError = function (stepnum, iserror) {
        if (typeof stepnum == "object") {
            iserror = stepnum.iserror;
            stepnum = stepnum.stepnum;
        }

        if (iserror) {
            $(this.steps.eq(stepnum - 1), this.target).addClass('error')
        } else {
            $(this.steps.eq(stepnum - 1), this.target).removeClass("error");
        }
    }

    SmartWizard.prototype.fixHeight = function () {
        var height = 0;

        var selStep = this.steps.eq(this.curStepIdx);
        var stepContainer = _step(this, selStep);
        stepContainer.children().each(function () {
            height += $(this).outerHeight();
        });

        // These values (5 and 20) are experimentally chosen.
        stepContainer.height(height + 10);
        this.elmStepContainer.height(height + 30);
    }

    _init(this);
};



(function ($) {

    $.fn.smartWizard = function (method) {
        var args = arguments;
        var rv = undefined;
        var allObjs = this.each(function () {
            var wiz = $(this).data('smartWizard');
            if (typeof method == 'object' || !method || !wiz) {
                var options = $.extend({}, $.fn.smartWizard.defaults, method || {});
                if (!wiz) {
                    wiz = new SmartWizard($(this), options);
                    $(this).data('smartWizard', wiz);
                }
            } else {
                if (typeof SmartWizard.prototype[method] == "function") {
                    rv = SmartWizard.prototype[method].apply(wiz, Array.prototype.slice.call(args, 1));
                    return rv;
                } else {
                    $.error('Method ' + method + ' does not exist on jQuery.smartWizard');
                }
            }
        });
        if (rv === undefined) {
            return allObjs;
        } else {
            return rv;
        }
    };

    // Default Properties and Events
    $.fn.smartWizard.defaults = {
        selected: 0,  // Selected Step, 0 = first step
        keyNavigation: true, // Enable/Disable key navigation(left and right keys are used if enabled)
        enableAllSteps: false,
        transitionEffect: 'fade', // Effect on navigation, none/fade/slide/slideleft
        contentURL: null, // content url, Enables Ajax content loading
        contentCache: true, // cache step contents, if false content is fetched always from ajax url
        cycleSteps: false, // cycle step navigation
        enableFinishButton: false, // make finish button enabled always
        hideButtonsOnDisabled: false, // when the previous/next/finish buttons are disabled, hide them instead?
        errorSteps: [],    // Array Steps with errors
        labelNext: 'Next',
        labelPrevious: 'Previous',
        labelFinish: 'Finish',
        noForwardJumping: false,
        onLeaveStep: null, // triggers when leaving a step
        onShowStep: null,  // triggers when showing a step
        onFinish: null  // triggers when Finish button is clicked
    };

})(jQuery);
