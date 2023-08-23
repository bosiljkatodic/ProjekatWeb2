import React, { useEffect, useState } from 'react';
import { GetArtiklePorudzbineProdavca } from '../Services/PorudzbinaService';
import { Link, useParams } from 'react-router-dom';





const Detalji = ({ match }) => {
  const [artikli, setArtikli] = useState([]);
  const { id } = useParams(); // Preuzimanje porudžbine ID iz rute

  const prodavac = JSON.parse(sessionStorage.getItem('korisnik'));
  const prodavacId = prodavac.id;
  

  useEffect(() => {
    const get = async () => {
      try {
        const resp = await GetArtiklePorudzbineProdavca(id, prodavacId);
        console.log(resp);
        console.log(resp);
        setArtikli(resp); 
       
        
      } catch (error) {
        console.error('Greska prilikom dobavljanja artikala porudzbine:', error);
      }
    };
    get();
  }, [id]);


  return (
    <div className="tabela-container">
      <h2>Moji artikli u porudzbini</h2>
      <table>
        <thead>
          <tr>
            <th>Id artikla</th>
            <th>Naziv</th>
            <th>Cijena</th>
            <th>Opis</th>
            <th>Slika</th>
          </tr>
        </thead>
        <tbody>
        {artikli.map((artikal) => (
            <tr key={artikal.id}>
              <td>{artikal.id}</td>
              <td>{artikal.naziv}</td>
              <td>{artikal.cijena}</td>
              <td>{artikal.opis}</td>
              <td>
              <img
                        className="ui big image"
                        src={artikal.fotografija}
                        width="200"
                        height="100"
                      ></img>
          </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Link to ="/prodavacDashboard"> Nazad </Link>
    </div>
  );
};

export default Detalji;
