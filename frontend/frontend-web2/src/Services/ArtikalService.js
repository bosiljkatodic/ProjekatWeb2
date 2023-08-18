import axios from 'axios';
import ArtikalModel from "../Models/ArtikalModel";

const baseUrl = process.env.REACT_APP_API_BACKEND;

export const GetAllArticles = async(token) => {
    try{
        const { data } = await axios.get(`${baseUrl}/articles`,
                {
                    headers: {
                    "Content-Type": "application/json",
                    Authorization : `Bearer ${token}`
                    },
                }
            );
        const artikli = data.map(artikal => {
            return new ArtikalModel(artikal);
        })   
        return artikli;
    }catch(err){
        alert("Nesto se desilo prilikom dobavljanja artikala");
        return null;
    }
}