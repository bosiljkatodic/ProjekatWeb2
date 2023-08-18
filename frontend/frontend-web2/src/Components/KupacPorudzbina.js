import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {CreatePorudzbina} from "../Services/PorudzbinaService.js";
import { Button } from "@mui/material";

export default function KupacPorudzbina() {
  const [adresaDostave, setAdresaDostave] = useState("");
  const [komentar, setKomentar] = useState("");
  const [error, setError] = useState(false);
  const [izabraniArtikli, setIzabraniArtikli] = useState(
    JSON.parse(sessionStorage.getItem("porudzbina"))
  );
  const [cijena, setCijena] = useState("");

  const navigate = useNavigate();

  useEffect(() => {
    const ukupnaCijenaPorudzbine = () => {
      var ceneDostaveArtikala = []; //u njega stavljam sve razlicite cene dostave jer ima razlicitih prodavaca
      var ukupnaCijenaDostave = 0;
      var ukupnaCijenaPorudzbine = 0;
      for (let i = 0; i < izabraniArtikli.length; i++) {
        if (!ceneDostaveArtikala.includes(izabraniArtikli[i].cijenaDostave)) {
          //odredimo da li je zabelezena cijena dostave
          ceneDostaveArtikala.push(izabraniArtikli[i].cijenaDostave); //ako nije ubaci je
          ukupnaCijenaDostave += izabraniArtikli[i].cijenaDostave; //i dodaj na ukupnu cenu dostave
        }
        ukupnaCijenaPorudzbine +=
          izabraniArtikli[i].kolicina * izabraniArtikli[i].cijena; //saberi klasika, cijena artikla puta kolicina artikla
      }
      ukupnaCijenaPorudzbine += ukupnaCijenaDostave; //na to dodaj cenu dostave
      setCijena(ukupnaCijenaPorudzbine);
    };
    ukupnaCijenaPorudzbine();
  }, [cijena]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const korisnik = JSON.parse(sessionStorage.getItem("korisnik"));
    const korisnikId = korisnik.id;

    const token = sessionStorage.getItem("token");

    if (adresaDostave.length === 0) {
      setError(true);
      return;
    }

    const elementiPorudzbine = [];
    for (let i = 0; i < izabraniArtikli.length; i++) {
      var porudzbinaArtikal = {
        idArtikal: izabraniArtikli[i].idArtikal,
        kolicina: izabraniArtikli[i].kolicina,
      };
      elementiPorudzbine.push(porudzbinaArtikal);
    }

    const porudzbinaDto = JSON.stringify({
      komentar,
      adresaDostave,
      korisnikId,
      elementiPorudzbine,
      cijena
    });

    
    const data = await CreatePorudzbina(porudzbinaDto, token);
    if(data !== null){
        console.log(data);
        alert("Vaša porudžbina je uspješno kreirana.");
        navigate('/kupacDashboard');
    
    } 
  };

  return (
    <div className="card">
      <form className="ui form" onSubmit={handleSubmit}>
        <h2 className="ui center aligned header">Potvrdite porudzbinu</h2>
        <div className="field">
          <div className="ui green message">
          <h3>Izabrani artikli</h3>
            <div className="ui green message" style={{marginRight: 43 + 'em', marginLeft:40 + 'em'}}>

            <ul className="list">
              {izabraniArtikli.map((izabraniArtikal) => (
                <li>{izabraniArtikal.kolicina} {izabraniArtikal.naziv} </li>
              ))}
            </ul>
            </div>
            <br />
            <h3 className="ui left aligned header">
              Cijena porudzbine:
              </h3>
               {cijena} dinara
            
          </div>
        </div>
        <br/>
        <div className="field">
          <h3>Adresa dostave</h3>
          <input
            type="text"
            name="adresaDostave"
            placeholder="Adresa dostave"
            value={adresaDostave}
            onChange={(e) => setAdresaDostave(e.target.value)}
          />
          {error && adresaDostave.length === 0 ? (
            <div className="ui pointing red basic label">
              Morate unijeti adresu dostave
            </div>
          ) : null}
        </div>
        <div className="field">
          <h3>Komentar</h3>
          <textarea
            className="textarea-resize"
            rows="6"
            placeholder="Unesite dodatne komentare, odnosno napomene"
            value={komentar}
            onChange={(e) => setKomentar(e.target.value)}
          />
        </div>
        <Button    
            variant="contained"
            type="submit">
          Porucite
        </Button>
      </form>
    </div>
  );
}
