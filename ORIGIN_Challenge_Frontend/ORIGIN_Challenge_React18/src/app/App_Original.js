import React, { useState, useEffect } from 'react';
import './App.css';
import classNames from 'classnames';
//--assets
import logoReact from '../assets/logo.svg';
import logoLinkedin from '../assets/linkedin-logo1.png';
//--componentes
import tecladoNumerico from './teclado-numerico/teclado-numerico';
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
      showAlert('WARN', error.response?.data?.message || 'Error', '');
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
      if (res.data.code === 200) {
        setPaso(3);
        showAlert("OK", res.data.message, "");
      } else showAlert("WARN", res.data.message, "");
    } catch (error) {
      setSpinner(false);
      showAlert("WARN", error.response?.data?.message || error.message, "");
    }
  };

  const verificarPin = async (numeroPin) => {
    setSpinner(true);
    try {
      const res = await tarjetaService.verificarPin(tarjetaSinGuiones, numeroPin);
      setSpinner(false);
      if (res.data.code === 200) {
        setPaso(4);
        showAlert("OK", res.data.message, "");
      } else showAlert("WARN", res.data.message, "");
    } catch (error) {
      setSpinner(false);
      if (error.response?.status === 423) {
        setPaso(1);
        showAlert("ERR", error.response?.data?.message || error.message, "");
      } else showAlert("WARN", error.response?.data?.message || error.message, "");
    }
  };

  const abrirBalance = async () => {
    try {
      const res = operacionesService.abrirBalance(tarjetaSinGuiones);
      if (res.data.code === 200) {
        setPaso(5);
        setDineroEnCuenta(res.data.data.dineroEnCuenta);
        setFechaVencimiento(res.data.data.fechaVencimiento);
        setOperaciones(res.data.data.operaciones);
      } else showAlert("WARN", res.data.message, "");
    } catch (error) {
      showAlert("WARN", error.response?.data?.message || error.message, "");
    }
  };

  const retirarDinero = async (numeroRetiro) => {
    try {
      const res = operacionesService.retirarDinero(tarjetaSinGuiones, numeroRetiro);
      if (res.data.code === 200) {
        setPaso(4);
        showAlert("OK", res.data.message, "");
      } else {
        showAlert("WARN", res.data.message, "");
      }
    } catch (error) {
      showAlert("WARN", error.response?.data?.message || error.message, "");
    }
  };

  const salirAplicacion = () => {
    setPaso(1);
    setFechaVencimiento(new Date());
    setDineroEnCuenta(0);
    setOperaciones([]);
  };

  return (
    <div className="App">
      <header className="toolbar">
        <img src={logoReact} className="App-logo" alt="logo" width={80} />
        <span>Bienvenido al CHALLENGE ORIGIN de Elias Vallejos</span>
        <div className="spacer"></div>
        <a aria-label="LinkedIn" target="_blank" href="https://linkedin.com/in/elias-vallejos-24001089/" title="LinkedIn" className="linkedin-icon">
          <img id="linkedin-logo" src={logoLinkedin} alt="LinkedIn Logo" width={10} />
        </a>
      </header>
      <main className="content">
        {modal.visible && (
          <div
            className={classNames('alert', {
              'show-alert': modal.visible,
              'hide-alert': !modal.visible,
              'alert-success': modal.tipo === 'OK',
              'alert-warning': modal.tipo === 'WARN',
              'alert-danger': modal.tipo === 'ERR'
            })}
            role="alert"
            style={{ marginTop: '-70px', position: 'fixed', width: '50%', zIndex: 1000 }}
          >
            <h4 className="alert-heading">{modal.titulo}</h4>
            <p>{modal.contenido}</p>
          </div>
        )}
        {spinner && (
          <div className="spinner-border text-primary" style={{ width: '180px', height: '180px', position: 'absolute', marginTop: '100px' }} role="status"></div>
        )}
        {!spinner && paso === 1 && (
          <div>
            <h4 className="card-container" style={{ marginBottom: '30px' }}>Aplicación</h4>
            <br />
            <button className="btn" onClick={agregarTarjetaAleatoria}>Agregar tarjetas aleatorias a la BD</button>
            <div className="card-container">
              <a className="card" onClick={() => setPaso(2)}>
                <span className="material-symbols-outlined">group</span>
                &nbsp;&nbsp;
                <span>ATM (cajero automático)</span>
                <svg className="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
                  <path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z" />
                </svg>
              </a>
            </div>
          </div>
        )}
        {!spinner && paso === 2 && (
          <div style={{ width: '50%' }}>
            <tecladoNumerico title="Número de Tarjeta" tipo="esTarjeta" format="XXXX-XXXX-XXXX-XXXX" onAccept={(type, number) => verificarNumero(type, number)} />
          </div>
        )}
        {!spinner && paso === 3 && (
          <div style={{ width: '50%' }}>
            <tecladoNumerico title="Número de PIN" tipo="esPin" format="XXXX" onAccept={(type, number) => verificarNumero(type, number)} />
          </div>
        )}
        {!spinner && paso === 4 && (
          <div>
            <div className="card-container" style={{ marginBottom: '30px' }}>
              <a className="card" onClick={abrirBalance}>
                <span className="material-symbols-outlined">currency_exchange</span>
                &nbsp;&nbsp;
                <span>Balance</span>
                <svg className="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
                  <path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z" />
                </svg>
              </a>
              <a className="card" onClick={() => setPaso(6)}>
                <span className="material-symbols-outlined">monetization_on</span>
                &nbsp;&nbsp;
                <span>Retirar</span>
                <svg className="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
                  <path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z" />
                </svg>
              </a>
              <a className="card" onClick={salirAplicacion}>
                <span className="material-symbols-outlined">logout</span>
                &nbsp;&nbsp;
                <span>Salir</span>
                <svg className="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
                  <path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z" />
                </svg>
              </a>
            </div>
          </div>
        )}
        {!spinner && paso === 5 && (
          <div style={{ marginTop: '-60px' }}>
            <h4 className="card-container" style={{ marginBottom: '30px' }}>Aplicación</h4>
            <br />
            <div className="card-container">
              <a className="card">
                <span className="material-symbols-outlined">currency_exchange</span>
                &nbsp;&nbsp;
                <span>Balance: {dineroEnCuenta}</span>
              </a>
              <a className="card">
                <span className="material-symbols-outlined">date_range</span>
                &nbsp;&nbsp;
                <span>Fecha de Vencimiento: {new Date(fechaVencimiento).toLocaleDateString()}</span>
              </a>
            </div>
            <div className="container">
              <table className="table table-striped">
                <thead>
                  <tr>
                    <th>Descripción</th>
                    <th>Fecha</th>
                    <th>Monto</th>
                  </tr>
                </thead>
                <tbody>
                  {operaciones.map((operacion, index) => (
                    <tr key={index}>
                      <td>{operacion.descripcion}</td>
                      <td>{new Date(operacion.fecha).toLocaleDateString()}</td>
                      <td>{operacion.monto}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        )}
        {!spinner && paso === 6 && (
          <div style={{ width: '50%' }}>
            <tecladoNumerico title="Cantidad a Retirar" tipo="esRetiro" format="XXXX" onAccept={(type, number) => verificarNumero(type, number)} />
          </div>
        )}
      </main>
    </div>
  );
};

export default App;
