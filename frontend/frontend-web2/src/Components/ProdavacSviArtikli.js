import React, {useState, useEffect} from "react";
import { DeleteArtikal, GetProdavceveArtikle } from "../Services/ArtikalService";
import { useNavigate } from "react-router-dom";

export default function ProdavacSviArtikli() {
  const [loading, setLoading] = useState(true);
  const [artikli, setArtikli] = useState([]);
  
  const navigate = useNavigate();

  useEffect(() => {
    const getArtikle = async () => {
        const prodavac = JSON.parse(sessionStorage.getItem('korisnik'));
        const prodavacId = prodavac.id;
        const token = sessionStorage.getItem('token');
        
        const response = await GetProdavceveArtikle(prodavacId, token);
        if(response !== null){
            setArtikli(response);
            console.log(response);
            setLoading(false);
        }
    }
    getArtikle();
  }, [])


  const handleClickPromeniArtikal = (e) => {
    const artikalId = e.target.id;
    navigate(`/prodavacSviArtikli/IzmeniArtikal/${artikalId}`);   
  }

  const handleClickObrisiArtikal = async (e) => {
    const stringId = e.target.name.split(' ')[1];
    const artikalId = parseInt(stringId);
    const token = sessionStorage.getItem('token');
    const response = await DeleteArtikal(artikalId, token);

    if(response !== null){
        alert("Uspesno obrisan artikal");

    }
  }

  return ( 
    <div className="verification-container">
      {loading && (
        <div className="loader-container">
          <div className="ui active inverted dimmer">
            <div className="ui large text loader">Ucitavanje Artikala</div>
          </div>
        </div>
      )}
      {!loading && (
        <div>
          <div>
          <table align="centre" width="800"  style={{marginLeft: 20 + 'em'}}>
            <thead>
              <tr>
                <th>Artikal</th>
                <th>Cijena</th>
                <th>
                Kolicina
                </th>
                <th>
                Cijena Dostave
                </th>
                <th>Opis</th>
                <th>Obrisite/Izmenite</th>
              </tr>
            </thead>
            <tbody>
              {artikli.map((artikal) => (
                <tr>
                  <td>
                    <h4 className="ui image header">
                      <img
                        className="ui big image"
                        src={artikal.fotografija}
                        width="200"
                        height="100"
                      ></img>
                      
                    </h4>
                  </td>
                  <td className="center aligned">
                    <div className="sub header">{artikal.cijena} dinara</div>
                  </td>
                  <td className="center aligned">
                    <div className="sub header">          
                                    {artikal.kolicina}
</div>
                  </td>
                  <td className="center aligned">
                  <div className="sub header">
                          {artikal.cijenaDostave} dinara
                        </div>
                  </td>

                  <td className="center aligned">{artikal.opis}</td>
                  <td className="center aligned">
                    <button
                      className="ui blue labeled icon button"
                      id={artikal.id}
                      onClick={(e) => handleClickPromeniArtikal(e)}
                    >
                      <i className="check icon"></i>
                      Promenite artikal
                    </button>{" "}
                    <br />
                    <button
                      className="ui red labeled icon button"
                      name={`obrisi ${artikal.id}`}
                      onClick={(e) => handleClickObrisiArtikal(e)}
                    >
                      <i className="x icon"></i>
                      Obrisite Artikal
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          </div>
        </div>
      )}
    </div>
  );
}
