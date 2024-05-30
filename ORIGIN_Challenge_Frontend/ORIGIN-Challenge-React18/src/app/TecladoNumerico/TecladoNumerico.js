import React, { useState, useEffect } from 'react';
import classNames from 'classnames';

const TecladoNumerico = ({ title, tipo, format, onAccept }) => {
  const [campoVisualizacion, setCampoVisualizacion] = useState(format);
  const [modal, setModal] = useState({
    visible: false,
    tipo: "",
    titulo: "",
    contenido: ""
  });

  useEffect(() => {
    setCampoVisualizacion(format);
  }, [format]);

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

  //----------------------------------INGRESO DE DATOS----------------------------------
  const tecleoNumero = (numero) => {
    // tarjeta
    if (tipo === 'esTarjeta') filtrarTarjeta(numero);
    // pin
    if (tipo === 'esPin') filtrarPin(numero);
    // retiro
    if (tipo === 'esRetiro') filtrarRetiro(numero);
  };

  const filtrarTarjeta = (numero) => {
    if (campoVisualizacion === format) {
      setCampoVisualizacion(numero);
    }
    if (campoVisualizacion.length < 19) {
      var barra = "";
      if (campoVisualizacion.length === 4 || campoVisualizacion.length === 9 || campoVisualizacion.length === 14) {
        barra = '-';
      }
      setCampoVisualizacion(campoVisualizacion + barra + numero);
    }
  };

  const filtrarPin = (numero) => {
    if (campoVisualizacion === format) {
      setCampoVisualizacion(numero);
    }
    if (campoVisualizacion.length < 4) {
      setCampoVisualizacion(campoVisualizacion + numero);
    }
  };

  const filtrarRetiro = (numero) => {
    setCampoVisualizacion("");
    if (campoVisualizacion === format) setCampoVisualizacion(numero);
    else setCampoVisualizacion(campoVisualizacion + numero);
  };

  //----------------------------------VERIFICAR FORMATO AL ACEPTAR----------------------------------
  const verificarFormato = () => {
    // tarjeta
    if (tipo === 'esTarjeta') verificarTarjeta();
    // pin
    if (tipo === 'esPin') verificarPin();
    // retiro
    if (tipo === 'esRetiro') verificarRetiro();
  };

  const verificarTarjeta = () => {
    if (campoVisualizacion.length === 19 && campoVisualizacion !== format) {
      const tarjetaSinGuiones = campoVisualizacion.replace(/-/g, '').trim();
      onAccept(tipo, tarjetaSinGuiones);
    } else {
      showAlert('WARN', "Por favor, ingrese una tarjeta válida.", '');
    }
  };

  const verificarPin = () => {
    if (campoVisualizacion.length === 4 && campoVisualizacion !== format) {
      onAccept(tipo, campoVisualizacion);
    } else {
      showAlert('WARN', "Por favor, ingrese un pin válido.", '');
    }
  };

  const verificarRetiro = () => {
    if (campoVisualizacion !== format) {
      onAccept(tipo, campoVisualizacion);
    } else {
      showAlert('WARN', "Por favor, ingrese una cantidad válida.", '');
    }
  };

  //----------------------------------LIMPIAR CAMPO----------------------------------
  const limpiarCampos = () => {
    setCampoVisualizacion(format);
  };

  return (
    <div>
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
      <h4 className="card-container" style={{ marginBottom: '30px' }}>{title}</h4>
      <br /><br />
      <input
        id="campoVisualizacion"
        name="campoVisualizacion"
        className="form-control btn-group"
        style={{ height: '40px' }}
        value={campoVisualizacion}
        disabled
      />
      <br /><br /><br />
      <div className="container card-container" style={{ width: '70%' }}>
        <div className="row col-12 mb-2">
          <button className="btn col-4" onClick={() => tecleoNumero('7')}>7</button>
          <button className="btn col-4" onClick={() => tecleoNumero('8')}>8</button>
          <button className="btn col-4" onClick={() => tecleoNumero('9')}>9</button>
        </div>
        <div className="row col-12 mb-2">
          <button className="btn col-4" onClick={() => tecleoNumero('4')}>4</button>
          <button className="btn col-4" onClick={() => tecleoNumero('5')}>5</button>
          <button className="btn col-4" onClick={() => tecleoNumero('6')}>6</button>
        </div>
        <div className="row col-12 mb-2">
          <button className="btn col-4" onClick={() => tecleoNumero('1')}>1</button>
          <button className="btn col-4" onClick={() => tecleoNumero('2')}>2</button>
          <button className="btn col-4" onClick={() => tecleoNumero('3')}>3</button>
        </div>
        <div className="row col-12 mb-2">
          <button className="btn col-4" onClick={() => tecleoNumero('0')}>0</button>
          <button className="btn col-4" onClick={() => verificarFormato()}>Aceptar</button>
          <button className="btn col-4" onClick={limpiarCampos}>Limpiar</button>
        </div>
      </div>
    </div>
  );
};

export default TecladoNumerico;
