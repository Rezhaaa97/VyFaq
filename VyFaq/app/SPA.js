"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var forms_1 = require("@angular/forms");
require("rxjs/add/operator/map");
var Kunde_1 = require("./Kunde");
var http_2 = require("@angular/http");
var SPA = /** @class */ (function () {
    function SPA(_http, fb) {
        this._http = _http;
        this.fb = fb;
        this.skjema = fb.group({
            id: [""],
            fornavn: [null, forms_1.Validators.compose([forms_1.Validators.required, forms_1.Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])],
            etternavn: [null, forms_1.Validators.compose([forms_1.Validators.required, forms_1.Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])],
            epost: [null, forms_1.Validators.compose([forms_1.Validators.required, forms_1.Validators.pattern("[0-9a-zA-ZøæåØÆÅ@_\\-. ]{2,30}")])],
            spm: [null, forms_1.Validators.compose([forms_1.Validators.required, forms_1.Validators.pattern("[0-9a-zA-ZøæåØÆÅ?.,!\\-. ]{2,300}")])],
        });
    }
    SPA.prototype.ngOnInit = function () {
        this.laster = true;
        this.hentAlleKunder();
        this.hentAlleSpm();
        this.visSkjema = false;
        this.visKundeListe = true;
        this.visSpml = false;
    };
    SPA.prototype.visSporsmal = function () {
        this.visSpml = true;
        this.visSkjema = false;
        this.visKundeListe = false;
    };
    SPA.prototype.visForside = function () {
        this.visSpml = false;
        this.visSkjema = true;
        this.visKundeListe = false;
    };
    SPA.prototype.visKundeSide = function () {
        this.visSpml = false;
        this.visSkjema = false;
        this.visKundeListe = true;
    };
    //Kontaktskjema
    SPA.prototype.hentAlleKunder = function () {
        var _this = this;
        this._http.get("api/kunde")
            .map(function (returData) {
            var JsonData = returData.json();
            return JsonData;
        })
            .subscribe(function (JsonData) {
            _this.alleKunder = [];
            if (JsonData) {
                for (var _i = 0, JsonData_1 = JsonData; _i < JsonData_1.length; _i++) {
                    var kundeObjekt = JsonData_1[_i];
                    _this.alleKunder.push(kundeObjekt);
                    _this.laster = false;
                }
            }
            ;
        }, function (error) { return alert(error); }, function () { return console.log("ferdig get-api/kunde"); });
    };
    ;
    //FAQ
    SPA.prototype.hentAlleSpm = function () {
        var _this = this;
        this._http.get("api/faqhjelp")
            .map(function (returData) {
            var JsonData = returData.json();
            return JsonData;
        })
            .subscribe(function (JsonData) {
            _this.alleSpml = [];
            if (JsonData) {
                for (var _i = 0, JsonData_2 = JsonData; _i < JsonData_2.length; _i++) {
                    var spmlObjekt = JsonData_2[_i];
                    _this.alleSpml.push(spmlObjekt);
                    _this.laster = false;
                }
            }
            ;
        }, function (error) { return alert(error); }, function () { return console.log("ferdig get-api/Faqhjelp"); });
    };
    ;
    SPA.prototype.vedSubmit = function () {
        if (this.skjemaStatus == "Registrere") {
            this.lagreKunde();
        }
        else {
            alert("Feil i applikasjonen!");
        }
    };
    SPA.prototype.registrerKunde = function () {
        this.skjema.setValue({
            id: "",
            fornavn: "",
            etternavn: "",
            epost: "",
            spm: "",
        });
        this.skjema.markAsPristine();
        this.visKundeListe = false;
        this.visSpml = false;
        this.skjemaStatus = "Registrere";
        this.visSkjema = true;
    };
    SPA.prototype.tilbakeTilListe = function () {
        this.visKundeListe = true;
        this.visSkjema = false;
    };
    SPA.prototype.lagreKunde = function () {
        var _this = this;
        var lagretKunde = new Kunde_1.Kunde();
        lagretKunde.fornavn = this.skjema.value.fornavn;
        lagretKunde.etternavn = this.skjema.value.etternavn;
        lagretKunde.epost = this.skjema.value.epost;
        lagretKunde.spm = this.skjema.value.spm;
        var body = JSON.stringify(lagretKunde);
        var headers = new http_2.Headers({ "Content-Type": "application/json" });
        this._http.post("api/kunde", body, { headers: headers })
            .map(function (returData) { return returData.toString(); })
            .subscribe(function (retur) {
            _this.hentAlleKunder();
            _this.visSkjema = false;
            _this.visKundeListe = true;
        }, function (error) { return alert(error); }, function () { return console.log("ferdig post-api/kunde"); });
    };
    ;
    SPA = __decorate([
        core_1.Component({
            selector: "min-app",
            templateUrl: "./app/SPA.html"
        }),
        __metadata("design:paramtypes", [http_1.Http, forms_1.FormBuilder])
    ], SPA);
    return SPA;
}());
exports.SPA = SPA;
//# sourceMappingURL=SPA.js.map