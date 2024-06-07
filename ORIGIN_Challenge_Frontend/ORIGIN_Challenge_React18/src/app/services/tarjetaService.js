import axios from "axios";

const API_URL = 'https://localhost:7112';

const insertarDatosAleatorios = async () => {
    const response = await axios.get(`${API_URL}/Tarjetas/InsertarDatosAleatorios`);
    return response.data;
};

const verificarTarjeta = async (numeroTarjeta) => {
    const response = await axios.get(`${API_URL}/Tarjetas/VerificarTarjeta?numeroTarjeta=${numeroTarjeta}`);
    return response.data;
};

const verificarPin = async (numeroTarjeta, numeroPin) => {
    const response = await axios.get(`${API_URL}/Tarjetas/VerificarPin?numeroTarjeta=${numeroTarjeta}&numeroPin=${numeroPin}`);
    return response.data;
};

export default { insertarDatosAleatorios, verificarTarjeta, verificarPin };
