// JavaScript source code

var check = function () {
    if (document.getElementById('pass1').value ==
        document.getElementById('pass2').value) {
        document.getElementById('messege').style.color = 'green';
        document.getElementById('messege').innerHTML = 'matching';
        document.getElementById('submit').disabled = false;
    } else {
        document.getElementById('messege').style.color = 'red';
        document.getElementById('messege').innerHTML = 'not matching';
        document.getElementById('submit').disabled = true;
    }

}

function verify() {

    var fn = document.getElementById("first name").value;
    var ln = document.getElementById("last name").value;
    var email = document.getElementById("email").value;
    var mobile = document.getElementById("mobile").value;
    var address = document.getElementById("address").value;
    var dob = document.getElementById("dob").value;
    var pass1 = document.getElementById("pass1").value;
    document.getElementById('messege').style.color = 'red';

    if ((fn == "") || (ln == "") || (email == "") || (mobile == "") || (address == "") || (dob == "") || (pass1 == "")) {
        document.getElementById('messege').innerHTML = "Please fill all the required feilds";
   
        return false;
    }

  if (!/^[a-zA-Z]+$/.test(fn)) {
        document.getElementById("messege").innerHTML = "Invalid First name";
        return false;
    }

    if (!/^[a-zA-Z]+$/.test(ln)) {
        document.getElementById("messege").innerHTML = "Invalid Last name";
        return false;
    }

    if (mobile.length != 10 || !/^[0-9]+$/.test(mobile)) {
        document.getElementById("messege").innerHTML = "Invalid Mobile number";
        return false;
    }

    if ((pass1.length < 8) || (!pass1.match(/[0-9]/g)) || (!pass1.match(/[A-Z]/g)) || (!pass1.match(/[a-z]/g)) ) {
        document.getElementById("messege").innerHTML = "Password should contain atleast one Uppercase letter,lowercase letter and a number  and should be of minimum length 8";
        return false;
    }


    return true;
}