import {Component, OnInit} from "@angular/core";
import { Http, Response } from '@angular/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import "rxjs/add/operator/map";
import {Kunde} from "./Kunde";
import {Headers} from "@angular/http";
import { Faqhjelp } from "./Faqhjelp";


@Component({
    selector: "min-app",
    templateUrl: "./app/SPA.html"
})
export class SPA {
    visSkjema: boolean;
    skjemaStatus: string;
    visKundeListe: boolean;
    visSpml: boolean;
    alleSpml: Array<Faqhjelp>; // Liste FAQ
    alleKunder: Array<Kunde>; // Alle spørsmål i kontaktliste
    skjema: FormGroup;
    laster: boolean;

    constructor(private _http: Http, private fb: FormBuilder) {
        this.skjema = fb.group
            ({
                id: [""],
                fornavn: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])],
                etternavn: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])],
                epost: [null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ@_\\-. ]{2,30}")])],
                spm: [null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ?.,!\\-. ]{2,300}")])],
            });
    }

    ngOnInit() {
        this.laster = true;
        this.hentAlleKunder();
        this.hentAlleSpm();
        this.visSkjema = false;
        this.visKundeListe = true;
        this.visSpml = false;
    }
    visSporsmal() {
        this.visSpml = true;
        this.visSkjema = false;
        this.visKundeListe = false;
    }
    visForside() {
        this.visSpml = false;
        this.visSkjema = true;
        this.visKundeListe = false;
    }
    visKundeSide() {
        this.visSpml = false;
        this.visSkjema = false;
        this.visKundeListe = true;
    }

    //Kontaktskjema

    hentAlleKunder() {
        this._http.get("api/kunde")
            .map(returData => {
                let JsonData = returData.json();
                return JsonData;
            })
            .subscribe(
                JsonData => {
                    this.alleKunder = [];
                    if (JsonData) {
                        for (let kundeObjekt of JsonData) {
                            this.alleKunder.push(kundeObjekt);
                            this.laster = false;
                        }
                    };
                },
                error => alert(error),
                () => console.log("ferdig get-api/kunde")
            );
    };
    tommelned(spml: Faqhjelp) {
        console.log("id er :", spml.id);
        var like = new Faqhjelp();
        like.spml = spml.spml;
        like.svar = spml.svar;
        like.unlike = spml.unlike + 1;
        like.like = spml.like;
        console.log("Like er : ", like.like);
        console.log("Objektet er :", like);
        var body: string = JSON.stringify(like);
        var headers = new Headers({ "Content-Type": "application/json" });

        this._http.put("api/faqhjelp/" + spml.id, body, { headers: headers })
            .map(returData => returData.toString())
            .subscribe(
                retur => {
                    this.hentAlleSpm();

                },
                error => alert(error),
                () => console.log("ferdig post-api/kunde")
            );
    }
    tommelopp(spml: Faqhjelp) {
        console.log("Hei tommelopp");
        console.log("id er :", spml.id);
        var like = new Faqhjelp();
        like.spml = spml.spml;
        like.svar = spml.svar;
        like.unlike = spml.unlike;
        like.like = spml.like + 1;
        console.log("Like er : ", like.like);
        console.log("Objektet er :", like);
        var body: string = JSON.stringify(like);
        var headers = new Headers({ "Content-Type": "application/json" });

        this._http.put("api/faqhjelp/" + spml.id, body, { headers: headers })
            .map(returData => returData.toString())
            .subscribe(
                retur => {
                    this.hentAlleSpm();
                   
                },
                error => alert(error),
                () => console.log("ferdig post-api/kunde")
            );

    }

    //FAQ

    hentAlleSpm() {
        this._http.get("api/faqhjelp")
            .map(returData => {
                let JsonData = returData.json();
                return JsonData;
            })
            .subscribe(
                JsonData => {
                    this.alleSpml = [];
                    if (JsonData) {
                        for (let spmlObjekt of JsonData) {
                            this.alleSpml.push(spmlObjekt);
                            this.laster = false;
                        }
                        console.log(JsonData);
                    };
                    
                },
                error => alert(error),
                () => console.log("ferdig get-api/Faqhjelp")
            );
    };


    vedSubmit() {
        if (this.skjemaStatus == "Registrere") {
            this.lagreKunde();
        }


        else {
            alert("Feil i applikasjonen!");
        }
    }

    registrerKunde() {

        this.skjema.setValue
            ({
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
    }

    tilbakeTilListe() {
        this.visKundeListe = true;
        this.visSkjema = false;
    }


    lagreKunde() {
        var lagretKunde = new Kunde();

        lagretKunde.fornavn = this.skjema.value.fornavn;
        lagretKunde.etternavn = this.skjema.value.etternavn;
        lagretKunde.epost = this.skjema.value.epost;
        lagretKunde.spm = this.skjema.value.spm;

        var body: string = JSON.stringify(lagretKunde);
        var headers = new Headers({ "Content-Type": "application/json" });

        this._http.post("api/kunde", body, { headers: headers })
            .map(returData => returData.toString())
            .subscribe(
                retur => {
                    this.hentAlleKunder();
                    this.visSkjema = false;
                    this.visKundeListe = true;
                },
                error => alert(error),
                () => console.log("ferdig post-api/kunde")
            );
    };


}