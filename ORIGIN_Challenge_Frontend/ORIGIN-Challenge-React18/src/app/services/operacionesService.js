import axios from "axios";

const API_URL = 'https://localhost:7112';

const abrirBalance = async (numeroTarjeta) => {
    const response = await axios.get(`${API_URL}/Operaciones/Balance?numeroTarjeta=${numeroTarjeta}`);
    return response.data;
};

const retirarDinero = async (numeroTarjeta, cantidadRetiro) => {
    const response = await axios.get(`${API_URL}/Operaciones/Retiro?numeroTarjeta=${numeroTarjeta}&cantidadRetiro=${cantidadRetiro}`);
    return response.data;
};

export default { abrirBalance, retirarDinero };