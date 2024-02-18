import React, { Compon, Component } from 'react';
import { FlatList, Image, ImageBackground, StyleSheet, Text, TouchableOpacity, View, StatusBar } from 'react-native';
import moment from 'moment';
import api from '../services/api'

import AsyncStorage from '@react-native-async-storage/async-storage';


export default class ListaConsulta extends Component {
    constructor(props) {
        super(props);
        this.state = {
            listaConsultas: [],
        };
    }



    buscarConsulta = async () => {
        try {
            const token = await AsyncStorage.getItem('userToken');

            const resposta = await api.get('/Consultas/Lista/Minhas', {
                headers: {
                    Authorization: 'Bearer ' + token
                }
            })
            if (resposta.status == 200) {
                const dados = resposta.data.listaConsulta
                this.setState({ listaConsultas: dados });
            }
        }

        catch (error) {
            console.warn(error)
        }


    };


    componentDidMount() {
        this.buscarConsulta();
    };


    logout = async () => {
        await AsyncStorage.removeItem('userToken');
        this.props.navigation.navigate('Login');
    }

    render() {
        return (

            <ImageBackground
                style={StyleSheet.absoluteFillObject}
                style={styles.fundoMedico}
            >

            <StatusBar 
            hidden={false}
            backgroundColor={'#45E1E6'}
            />

                <TouchableOpacity onPress={this.logout} >
                    <Text style={styles.logout} >Logout</Text>
                </TouchableOpacity>

                <View style={styles.container}>
                    <TouchableOpacity onPress={this.buscarConsulta} >
                        <Image
                            source={require('../../assets/images/logo.png')}
                            style={styles.logoSp}
                        />
                    </TouchableOpacity>



                    <View style={styles.containerFlatList}>
                        <FlatList
                            contentContainerStyle={styles.mainBodyContent}
                            data={this.state.listaConsultas}
                            keyExtractor={item => item.idConsulta}
                            renderItem={this.renderItem}
                        />
                    </View>
                </View>

            </ImageBackground>
        )
    }

    renderItem = ({ item }) => (
        <View style={styles.teste}>
            <View style={styles.card}>
                <View style={styles.tituloCardWrapper}>
                    <Text>Consulta</Text>
                    <Text>{item.idConsulta}</Text>
                </View>
                <View style={styles.textoCardWrapper}>
                    <View style={styles.container_dados}>
                        <Text style={styles.tituloTexto}>Paciente</Text>
                        <Text style={styles.dados}>{item.idPacienteNavigation.idUsuarioNavigation.nome}</Text>
                    </View>
                    <View style={styles.container_dados}>
                        <Text style={styles.tituloTexto}>Medico</Text>
                        <Text style={styles.dados}>{item.idMedicoNavigation.idUsuarioNavigation.nome}</Text>
                    </View>
                    <View style={styles.container_dados}>
                        <Text style={styles.tituloTexto}>Situação</Text>
                        <Text style={styles.dados}>{item.idSituacaoNavigation.descricao}</Text>
                    </View>
                    <View style={styles.container_dados}>
                        <Text style={styles.tituloTexto}>Data da Consulta</Text>
                        <Text style={styles.dados}>{moment(item.dataConsulta).format('L')}</Text>
                    </View>
                    <View style={styles.container_dados}>
                        <Text style={styles.tituloTexto}>Descrição</Text>
                        <Text style={styles.dados}>{item.descricao}</Text>
                    </View>
                </View>
            </View>
        </View>
    )

};



const styles = StyleSheet.create({

    logout: {
        backgroundColor: '#45E1E6',
        borderRadius: 10,
        width: 62,
        height: 31,
        padding: 5,
        paddingLeft: 8,
        marginLeft: 8,
        top: 7,
    },

    container: {
        flex: 1,
        alignItems: 'center'
    },

    fundoMedico: {
        backgroundColor: '#3D8DF2',
        flex: 1,
    },

    container_dados: {
        flexDirection: 'row',
        justifyContent: 'space-around',
        width: '90%',
        height: 25,
    },

    textoCardWrapper: {
        backgroundColor: '#fff',
        borderBottomLeftRadius: 10,
        borderBottomRightRadius: 10,
        padding: 20,
    },

    dados: {
        fontSize: 15,
        color: 'black',
    },

    logoSp: {
        width: 500,
        height: 190,
    },

    retangulo: {
        width: 300,
        height: 100,
        backgroundColor: 'red',
    },

    containerFlatList: {
        flex: 1,
        alignItems: 'center',
        justifyContent: 'center',
        marginTop: 20
    },

    teste: {
        alignItems: 'center'
    },

    mainBodyContent: {
        justifyContent: 'space-around',
    },

    card: {
        alignItems: 'center',
        width: '85%',
        marginBottom: 30
    },

    tituloCardWrapper: {
        backgroundColor: '#45E1E6',
        height: 50,
        width: 315,
        borderTopRightRadius: 5,
        borderTopLeftRadius: 5,
        justifyContent: 'center',
        alignItems: 'center',
    },

    tituloTexto: {
        fontWeight: '800',
    }

})