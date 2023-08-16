import axios from 'axios';
import IspisModel from "../Models/IspisModel";
import KorisnikModel from '../Models/KorisnikModel';

const baseUrl = process.env.REACT_APP_API_BACKEND;


export const LoginUser = async (email, lozinka) => {
    //const url_login = "users/login";

    try {
        const {data} = await axios.post(`${baseUrl}/users/login`,
        JSON.stringify({ email, lozinka }),
        {
            headers: { "Content-Type": "application/json" },
            withCredentials: true,
        }
        );
        const odgovor = new IspisModel(data);
        return odgovor;
    } catch(error){
        alert("Greska pri logovanju");
        return null;
    }
}

export const RegisterUser = async (korisnikJSON) => {
    try{
        const {data} = await axios.post(`${baseUrl}/users/registration`,
            korisnikJSON,
            {
                headers:{'Content-Type' : 'application/json'},
                withCredentials: true
            }
        );
        const response = new IspisModel(data);
        return response;
    }catch(err){
        alert("Nesto se desilo prilikom registracije");
        return null;
    }
}

export const IzmijeniProfil = async (updatedKorisnikJSON, id, token) => {
    try{
        const {data} = await axios.put(`${baseUrl}/users/${id}`, updatedKorisnikJSON,
            {
                headers: 
                {
                    'Content-Type' : 'application/json',
                    'Authorization' : token
                },
                withCredentials: true
            }
        );
        //const izmijenjenKorisnik = new KorisnikModel(data);
        const updatedKorisnik = new KorisnikModel(data);
        return updatedKorisnik;
    }catch(err){
        console.log(err);
        alert("Nesto se desilo prilikom izmjene podataka")
    }
}

export const getKorisnikId = async (id) => {
    try {
      const {data} = await axios.get(`${baseUrl}/users/${id}`);
      const response = new KorisnikModel(data);
        return response;
    } catch (error) {
      console.error(error);
      throw new Error('Greska prilikom dobavljanja informacija o korisniku.');
    }
  };