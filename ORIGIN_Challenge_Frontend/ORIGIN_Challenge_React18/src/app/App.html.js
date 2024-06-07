import './App.css';
import classNames from 'classnames';
import GroupIcon from '@material-ui/icons/Group';
//--assets
import logoReact from '../assets/logo.svg';
import logoLinkedin from '../assets/linkedin-logo1.png';
import logoTarjeta from '../assets/tarjeta.png';
//--componentes
import TecladoNumerico from './TecladoNumerico/TecladoNumerico';

const AppHTML = ({
  modal,
  spinner,
  paso,
  showAlert,
  setPaso,
  agregarTarjetaAleatoria,
  abrirBalance,
  verificarNumero,
  salirAplicacion,
  tarjetaSinGuiones,
  dineroEnCuenta,
  fechaVencimiento,
  operaciones
}) => {

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

        <br />
        <br />
        <br />

        {/* <!-- paso 1: entrar en la aplicacion --> */}
        {!spinner && paso === 1 && (
          <div>
            <h4 className="card-container" style={{ marginBottom: '30px' }}>Aplicación</h4>
            <br />
            <button className="btn btn-primary" onClick={agregarTarjetaAleatoria}>Agregar tarjetas aleatorias a la BD</button>
            <div className="card-container">
              <a className="card" onClick={() => setPaso(2)}>
                <GroupIcon className="material-symbols-outlined">group</GroupIcon>
                &nbsp;&nbsp;
                <span>ATM (cajero automático)</span>
                <svg className="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
                  <path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z" />
                </svg>
              </a>
            </div>
          </div>
        )}

        {/* <!-- paso 2: ingresar numero de tarjeta --> */}
        {!spinner && paso === 2 && (
          <div style={{ width: '50%' }}>
            <TecladoNumerico title="Número de Tarjeta" tipo="esTarjeta" format="XXXX-XXXX-XXXX-XXXX" onAccept={(tipo, numero) => verificarNumero(tipo, numero)} ></TecladoNumerico>
          </div>
        )}

        {/* <!-- paso 3: ingresar numero de pin --> */}
        {!spinner && paso === 3 && (
          <div style={{ width: '50%' }}>
            <TecladoNumerico title="Número de PIN" tipo="esPin" format="XXXX" onAccept={(tipo, numero) => verificarNumero(tipo, numero)} ></TecladoNumerico>
          </div>
        )}

        {/* <!-- paso 4: elegir la operacion --> */}
        {!spinner && paso === 4 && (
          <div>
            <div className="card-container" style={{ marginBottom: '30px' }}>
              <a className="card" onClick={abrirBalance}>
                <GroupIcon className="material-symbols-outlined">currency_exchange</GroupIcon>
                &nbsp;&nbsp;
                <span>Balance</span>
                <svg className="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
                  <path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z" />
                </svg>
              </a>
              <a className="card" onClick={() => setPaso(6)}>
                <GroupIcon className="material-symbols-outlined">monetization_on</GroupIcon>
                &nbsp;&nbsp;
                <span>Retirar</span>
                <svg className="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
                  <path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z" />
                </svg>
              </a>
            </div>
          </div>
        )}

        {/* <!-- paso 5: ver el balance --> */}
        {!spinner && paso === 5 && (
          <div>
            <div className="container p-4 d-flex justify-content-center">
              <div className="row" style={{ width: '600px' }}>
                <div className="col-6 d-flex justify-content-center" style={{ marginTop: '5%' }}>
                  {spinner && (
                    <div
                      className="spinner-border text-primary"
                      role="status"
                      style={{ width: '180px', height: '180px', position: 'absolute', marginTop: '-10px' }}
                    />
                  )}
                  <img src={logoTarjeta} alt="Imagen de tarjeta" width="200" style={{ marginTop: '-20px' }} />
                </div>
                <div className="col-6" style={{ marginTop: '20px' }}>
                  <form className="d-flex flex-column col-12" style={{ width: '100%' }}>
                    <label htmlFor="numeroTarjeta">Número Tarjeta:</label>
                    <input
                      id="numeroTarjeta"
                      name="numeroTarjeta"
                      className="form-control btn-group"
                      style={{ height: '40px' }}
                      value={tarjetaSinGuiones}
                      disabled
                    />
                    <label htmlFor="fechaVencimiento">Fecha Vencimiento:</label>
                    <input
                      id="fechaVencimiento"
                      name="fechaVencimiento"
                      className="form-control btn-group"
                      style={{ height: '40px' }}
                      value={fechaVencimiento}
                      disabled
                    />
                    <label htmlFor="balance">Dinero en Cuenta:</label>
                    <input
                      id="balance"
                      name="balance"
                      className="form-control btn-group"
                      style={{ height: '40px' }}
                      value={dineroEnCuenta}
                      disabled
                    />
                  </form>
                </div>
              </div>
            </div>
            {operaciones.length > 0 && (
              <div className="row container" style={{ width: '120%', marginLeft: '-6%', marginTop: '6%' }}>
                <div className="d-flex justify-content-center highlight-card table table-primary" style={{ width: '90%', height: '100%' }}>
                  <table>
                    <thead>
                      <tr>
                        <th>Fecha Operación</th>
                        <th>Código Operación</th>
                        <th>Retiros</th>
                        <th>Balance</th>
                      </tr>
                    </thead>
                    <tbody>
                      {operaciones.map((operacion, index) => (
                        <tr key={index}>
                          <td style={{ minWidth: '250px' }}>{operacion.fecha}</td>
                          <td style={{ minWidth: '170px' }}>{operacion.codigoOperacion}</td>
                          <td style={{ minWidth: '100px' }}>{operacion.cantidadRetiro}</td>
                          <td style={{ minWidth: '100px' }}>{operacion.balance}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>
            )}
            <div className="d-flex justify-content-center">
              <div className="btn btn-primary" style={{ marginTop: '50px', marginBottom: '-40px' }} onClick={() => setPaso(4)}>
                Atrás
              </div>
            </div>
          </div>
        )}

        {/* <!-- paso 6: realizar un retiro --> */}
        {!spinner && paso === 6 && (
          <div style={{ width: '50%' }}>
            <TecladoNumerico title="Cantidad a Retirar" tipo="esRetiro" format="0.00" onAccept={(tipo, numero) => verificarNumero(tipo, numero)} ></TecladoNumerico>
            <div className="d-flex justify-content-center">
              <div className="btn btn-primary" style={{ marginTop: '50px', marginBottom: '-40px' }} onClick={() => setPaso(4)}>
                Atrás
              </div>
            </div>
          </div>
        )}

        <button className="btn btn-primary" style={{ marginTop: '50px' }} onClick={() => salirAplicacion()}>Salir</button>
      </main >
    </div >
  );
};

export default AppHTML;
