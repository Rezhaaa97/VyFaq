import {Component, OnInit} from "@angular/core";
import { Http, Response } from '@angular/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import "rxjs/add/operator/map";
import {Kunde} from "./Kunde";
import {Headers} from "@angular/http";


@Component({
    selector: "min-app",
    templateUrl: "./app/SPA.html"
})
export class SPA {
    visSkjema: boolean;
    skjemaStatus: string;
    visKundeListe: boolean;
    alleKunder: Array<Kunde>; // for listen av alle kundene
    skjema: FormGroup;
    laster: boolean;
    
    constructor(private _http: Http, private fb: FormBuilder) {
        this.skjema = fb.group({
            id: [""],
            fornavn: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])],
            etternavn: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])],
            adresse: [null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\-. ]{2,30}")])],
            postnr: [null, Validators.compose([Validators.required, Validators.pattern("[0-9]{4}")])],
            poststed: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])]
        });
    }
   
    ngOnInit() {
        this.laster = true;
        this.hentAlleKunder();
        this.visSkjema = false;
        this.visKundeListe = true;
    }

    hentAlleKunder() {
        this._http.get("api/kunde/")
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

    vedSubmit() {
        if (this.skjemaStatus == "Registrere") {
            this.lagreKunde();
        }
        else if (this.skjemaStatus == "Endre") {
            this.endreEnKunde();
        }
        else {
            alert("Feil i applikasjonen!");
        }
    }

    registrerKunde() {
        // må resette verdiene i skjema dersom skjema har blitt brukt til endringer

        this.skjema.setValue({
            id: "",
            fornavn: "",
            etternavn: "",
            adresse: "",
            postnr: "",
            poststed:""
        });
        this.skjema.markAsPristine();
        this.visKundeListe = false;
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
        lagretKunde.adresse = this.skjema.value.adresse;
        lagretKunde.postnr = this.skjema.value.postnr;
        lagretKunde.poststed = this.skjema.value.poststed;

        var body: string = JSON.stringify(lagretKunde);
        var headers = new Headers({ "Content-Type": "application/json" });

        this._http.post("api/kunde", body, { headers: headers })
            .map(returData => returData.toString())
            .subscribe(
                retur=> {
                    this.hentAlleKunder();
                    this.visSkjema = false;
                    this.visKundeListe = true;
                },
            error => alert(error),
            () => console.log("ferdig post-api/kunde")
        );
    };

    sletteKunde(id: number) {
        this._http.delete("api/kunde/" + id)
            .map(returData => returData.toString())
            .subscribe(
            retur => {
                this.hentAlleKunder();
            },
            error => alert(error),
            () => console.log("ferdig delete-api/kunde")
        );
    };
    // her blir kunden hentet og vist i skjema
    endreKunde(id: number) {
        this._http.get("api/kunde/"+id)
            .map(returData => {
                let JsonData = returData.json();
                return JsonData;
            })
            .subscribe(
            JsonData => { // legg de hentede data inn i feltene til endreSkjema. Kan bruke setValue også her da hele skjemaet skal oppdateres. 
                this.skjema.patchValue({ id: JsonData.id });
                this.skjema.patchValue({ fornavn: JsonData.fornavn });
                this.skjema.patchValue({ etternavn: JsonData.etternavn });
                this.skjema.patchValue({ adresse: JsonData.adresse });
                this.skjema.patchValue({ postnr: JsonData.postnr });
                this.skjema.patchValue({ poststed: JsonData.poststed });
                },
            error => alert(error),
            () => console.log("ferdig get-api/kunde")
        );
        this.skjemaStatus = "Endre";
        this.visSkjema = true;
        this.visKundeListe = false;
    }
    // her blir den endrede kunden lagret
    endreEnKunde() {
        var endretKunde = new Kunde();

        endretKunde.fornavn = this.skjema.value.fornavn;
        endretKunde.etternavn = this.skjema.value.etternavn;
        endretKunde.adresse = this.skjema.value.adresse;
        endretKunde.postnr = this.skjema.value.postnr;
        endretKunde.poststed = this.skjema.value.poststed;

        var body: string = JSON.stringify(endretKunde);
        var headers = new Headers({ "Content-Type": "application/json" });

        this._http.put("api/kunde/" + this.skjema.value.id, body, { headers: headers })
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
    }
}