import React from 'react';
import { Link } from "react-router-dom";

import api from '../../services/api';
import '../../Assets/CSS/consultasAdm.css';
import HeaderAdm from '../../components/header/headerAdm';

import logo from '../../Assets/img/Sp Medical Grouplogo.svg';


class cadastrarMapa extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            listaPacientes: [],
            idPaciente: 0,
            nomePaciente: '',
            longitude: '',
            latitude: '',
            active: false,
            isLoading: false
        };
    };

    toggleMode = () => {
        this.setState({ active: !this.state.active })
    }

    buscarPacientes = async () => {
        try {

            const resposta = await api.get('/Pacientes', {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('usuario-login')
                }
            })
            if (resposta.status === 200) {
                this.setState({ listaPacientes: resposta.data.lista });
            }
        }

        catch (error) {
            console.warn(error)
        }
    };

    componentDidMount() {
        this.buscarPacientes();
    };

    logout = async () => {
        localStorage.removeItem('usuario-login');
        this.props.history.push('/');
    };



    atualizaStateCampo = (campo) => {
        this.setState({ [campo.target.name]: campo.target.value });
    };





    cadastrarLocalização = item => {
        try {
            this.setState({ isLoading: true });


            const resposta = api.post('/Localizacoes', {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('usuario-login')
                }
            }, {
                Nome: this.state.nomePaciente,
                latitude: this.state.latitude,
                Longitude: this.state.longitude,
            },
                {
                    headers: {
                        Authorization: 'Bearer ' + localStorage.getItem('usuario-login'),
                    },
                },
            );
            if (resposta.status === 201) {
                console.warn('Localização realizada com sucesso.');
            } else {
                console.warn('Falha ao Cadastrar uma Localização.');
            }
        } catch (error) {
            console.warn(error);
        }


    };


    render() {
        return (
            //JSX
            <div>

                <header>
            <HeaderAdm />

                </header>

                <main className="afastar_list_consulta ">
                    {/* Cadastro de Localizações */}
                    <div className="afastar_list_consulta">
                        <section className="cadastro_consulta grid ">
                            <h2 className="letra_tam">Cadastro de Localizações</h2>
                            <div className="cadastro_ajuste_consulta">
                                <form onSubmit={this.cadastrarLocalização} >
                                    <div>

                                        <select
                                            className="input_consulta"
                                            name="nomePaciente"
                                            value={this.state.nomePaciente}
                                            onChange={this.atualizaStateCampo}>
                                            <option value="0">Selecione o paciente</option>

                                            {
                                                this.state.listaPacientes.map((paciente) => {
                                                    return (
                                                        <option key={paciente.nomePaciente} value={paciente.nomePaciente}>
                                                            {paciente.idUsuarioNavigation.nome}
                                                        </option>
                                                    )
                                                })
                                            }
                                        </select>

                                        <input
                                            className="input_consulta_descricao "
                                            placeholder="Latitude"
                                            type="number"
                                            name="latitude"
                                            value={this.state.latitude}
                                            onChange={this.atualizaStateCampo}
                                        />

                                        <input
                                            className="input_consulta_descricao"
                                            placeholder="Longitude"
                                            type="number"
                                            name="longitude"
                                            value={this.state.longitude}
                                            onChange={this.atualizaStateCampo}
                                        />

                                        <div className="btn_cadastrar_consulta">

                                            {this.state.isLoading && (
                                                <button className="btn_consultaAdm " disabled>
                                                    Loading...{' '}
                                                </button>
                                            )}

                                            {this.state.isLoading === false && (
                                                <button className="btn_consultaAdm btn" >Cadastrar</button>
                                            )}
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </section>
                    </div>


                </main>

                <footer>
                    <div className="container_footer afastar_list_consulta">
                        <div className="center_footer">
                            <Link to="/">
                                <img
                                    src={logo}
                                    className="icone_consulta_footer"
                                    alt="logo da Sp Medical Group"
                                />{' '}
                            </Link>
                            <span className="span_footer">Feito por Senai de Informática</span>
                        </div>
                    </div>
                </footer>
            </div>
        )
    }
};
export default cadastrarMapa;