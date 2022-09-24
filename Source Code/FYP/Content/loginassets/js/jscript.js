   const togglePassword = document.querySelector('#togglePassword');
const password = document.querySelector('#password');
togglePassword.addEventListener('click', function (e) {
    // toggle the type attribute
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    // toggle the eye / eye slash icon
    this.classList.toggle('bi-eye');
});

//function submit() {
  
//}
//$("#password").keyup(function(){
//  if($(this).val() == "") {
//      $("#btnsubmit").show();
//  } else {
//      $("#btnsubmit").hide();
//  }
//});