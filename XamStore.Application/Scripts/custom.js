var online = window.navigator.onLine;


/*FACEBOOK*/
FB.init({
    appId: '829575857090769',
    status: true, cookie: false, xfbml: true, oauth: true
});

$(function () {
    $('.button-submit-facebook').on("click", function () {
        Login();
    });

    $(".reg_tw").click(function () {
        startApp();
    
    });
})


function Login() {
    if (!online) {
        alert("Houve um erro ao se conectar com o Facebook, falha ao se conectar com a internet");

        return false;
    }

    FB.login(function (response) {
        if (response.authResponse) {
            FB.api('/me', function (response) {
                //indow.location.replace();
                $.ajax({
                    type: "POST",
                    data: { FacebookId: response.id, Email: response.email, NomeRazao: response.name },
                    url: "Login/Facebook",
                    success: function (data) {
                        window.location.replace(data.RedirectUrl);
                    }
                });
            });

        }

    }, { scope: 'email,user_photos,publish_actions' });
    
}

/* GOOGLE */
var onSuccess = function (user) {
    var usuario = user.getBasicProfile();
    $.ajax({
        type: "POST",
        data: { GoogleId: usuario.wc, Email: usuario.po, NomeRazao: usuario.zt },
        url: "Login/Google",


    

        success: function (data) {
            window.location.replace(data.RedirectUrl);
        }
    });
};


var onFailure = function (error) {
    console.log(error);
};

var googleUser = {};
var startApp = function () {

    if (!online) {
        alert("Houve um erro ao se conectar ao goole+, falha ao se conectar com a internet.");

        return false;
    }

    gapi.load('auth2', function () {
        auth2 = gapi.auth2.init({
            client_id: '1035537579914-su809tar1augpqpj0hcrl2tmgdc080r0.apps.googleusercontent.com',
            cookiepolicy: 'single_host_origin',
        });

        auth2.attachClickHandler('signin-button', {}, onSuccess, onFailure);

    });
};
