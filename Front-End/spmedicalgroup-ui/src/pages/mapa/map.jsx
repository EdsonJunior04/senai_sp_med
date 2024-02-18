// import { Map, Marker, GoogleApiWrapper, InfoWindow } from 'google-maps-react';
// import { Component } from "react";
// import api from '../../services/api';
// import HeaderAdm from '../../components/header/headerAdm';

// class maps extends Component {
//     constructor(props) {
//         super(props);
//         this.state = {
//             listaLocalizacoes: [],
//             showingInfoWindow: false,
//             marcadorAtivo: {},
//             lugar: {},
//             active: false,
//         }
//     };

//     BuscarLocalizacoes = () => {
//         api("/Localizacoes", {
//             headers: {
//                 Authorization: 'Bearer ' + localStorage.getItem('usuario-login')
//             }
//         })
//             .then(resposta => {
//                 if (resposta.status === 200) {
//                     this.setState({ listaLocalizacoes: resposta.data });
//                 }
//             }).catch(erro => console.log(erro))
//     }

//     cliqueMarcador = (props, marker, e) =>
//         this.setState({
//             lugar: props,
//             marcadorAtivo: marker,
//             showingInfoWindow: true,

//         });

//     toggleMode = () => {
//         this.setState({ active: !this.state.active })
//     }

//     componentDidMount() {
//         this.BuscarLocalizacoes()
//     }
//     logout = async () => {
//         localStorage.removeItem('usuario-login');
//         this.props.history.push('/');
//     };

//     render() {
//         return (
//             <div>
//                 <header>
//                     <HeaderAdm />
//                 </header>
//                 <main>           

//                     <Map google={this.props.google} zoom={12}
//                         initialCenter={{
//                             lat: -23.53642760296254,
//                             lng: -46.64621432441258
//                         }}>

//                         {

//                             this.state.listaLocalizacoes.map((item) => {

//                                 return (
//                                     <Marker onClick={this.cliqueMarcador}
//                                         id={item.id}
//                                         name={item.nome}
//                                         title={item.nome}
//                                         position={{ lat: item.latitude, lng: item.longitude }} />
//                                 )
//                             })
//                         }

//                         <InfoWindow
//                             marker={this.state.marcadorAtivo}
//                             visible={this.state.showingInfoWindow}>
//                             <div>
//                                 <h1 style={{ fontSize: 14, color: "#82C0D9" }}>{this.state.lugar.name}</h1>
//                             </div>
//                         </InfoWindow>

//                     </Map>
//                 </main>
//             </div>
//         )
//     }

// }

// export default GoogleApiWrapper({
//     apiKey: ("AIzaSyBBZYzs6HaSyjeVDFe-6UuasHX7XSB3Z5E")
// })(maps)