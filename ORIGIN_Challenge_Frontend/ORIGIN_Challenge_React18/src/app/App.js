import React, { useState } from 'react';
import AppHTML from './App.html.js';
import './App.css';
//--servicios
import tarjetaService from './services/tarjetaService';
import operacionesService from './services/operacionesService';

const App = () => {
  const [spinner, setSpinner] = useState(false);
  const [paso, setPaso] = useState(1);
  const [tarjetaSinGuiones, setTarjetaSinGuiones] = useState('');
  const [fechaVencimiento, setFechaVencimiento] = useState(new Date());
  const [dineroEnCuenta, setDineroEnCuenta] = useState(0);
  const [operaciones, setOperaciones] = useState([]);
  const [modal, setModal] = useState({
    visible: false,
    tipo: "",
    titulo: "",
    contenido: ""
  });

  const showAlert = (tipo, titulo, contenido) => {
    setModal({
      visible: true,
      tipo: tipo,
      titulo: titulo,
      contenido: contenido
    });
    setTimeout(() => {
      setModal((prevState) => ({ ...prevState, visible: false }));
    }, 2500);
  };

  const agregarTarjetaAleatoria = async () => {
    setSpinner(true);
    try {
      const res = await tarjetaService.insertarDatosAleatorios();
      setSpinner(false);
      if (res.code === 200) showAlert('OK', res.message, '');
      else showAlert('WARN', res.message, '');
    } catch (error) {
      setSpinner(false);
      showAlert('WARN', error.response?.data, '');
    }
  };

  const verificarNumero = (tipo, numero) => {
    if (tipo === 'esTarjeta') {
      verificarTarjeta(numero);
    } else if (tipo === 'esPin') {
      verificarPin(numero);
    } else if (tipo === 'esRetiro') {
      retirarDinero(numero);
    }
  };

  const verificarTarjeta = async (numeroTarjeta) => {
    setSpinner(true);
    setTarjetaSinGuiones(numeroTarjeta);
    try {
      const res = await tarjetaService.verificarTarjeta(numeroTarjeta);
      setSpinner(false);
      if (res.code === 200) {
        setPaso(3);
        showAlert("OK", res.message, "");
      } else showAlert("WARN", res.message, "");
    } catch (error) {
      setSpinner(false);
      showAlert("WARN", error.response?.data, "");
    }
  };

  const verificarPin = async (numeroPin) => {
    setSpinner(true);
    try {
      const res = await tarjetaService.verificarPin(tarjetaSinGuiones, numeroPin);
      setSpinner(false);
      if (res.code === 200) {
        setPaso(4);
        showAlert("OK", res.message, "");
      } else showAlert("WARN", res.message, "");
    } catch (error) {
      setSpinner(false);
      if (error.response?.status === 423) {
        setPaso(1);
        showAlert("ERR", error.response?.data, "");
      } else showAlert("WARN", error.response?.data, "");
    }
  };

  const abrirBalance = async () => {
    try {
      const res = await operacionesService.abrirBalance(tarjetaSinGuiones);
      if (res.code == 200) {
        setPaso(5);
        setDineroEnCuenta(res.data.dineroEnCuenta);
        setFechaVencimiento(res.data.fechaVencimiento);
        setOperaciones(res.data.operaciones);
      } else showAlert("WARN", res.message, "");
    } catch (error) {
      showAlert("WARN", error.response?.data, "");
    }
  };

  const retirarDinero = async (numeroRetiro) => {
    try {
      const res = await operacionesService.retirarDinero(tarjetaSinGuiones, numeroRetiro);
      if (res.code === 200) {
        setPaso(4);
        showAlert("OK", res.message, "");
      } else showAlert("WARN", res.message, "");
    } catch (error) {
      showAlert("WARN", error.response?.data, "");
    }
  };

  const salirAplicacion = () => {
    setPaso(1);
    setFechaVencimiento(new Date());
    setDineroEnCuenta(0);
    setOperaciones([]);
  };

  return (
    <AppHTML
      modal={modal}
      spinner={spinner}
      paso={paso}
      showAlert={showAlert}
      setPaso={setPaso}
      agregarTarjetaAleatoria={agregarTarjetaAleatoria}
      abrirBalance={abrirBalance}
      verificarNumero={verificarNumero}
      salirAplicacion={salirAplicacion}
      tarjetaSinGuiones={tarjetaSinGuiones}
      dineroEnCuenta={dineroEnCuenta}
      fechaVencimiento={fechaVencimiento}
      operaciones={operaciones}
    />
  );
};

export default App;
