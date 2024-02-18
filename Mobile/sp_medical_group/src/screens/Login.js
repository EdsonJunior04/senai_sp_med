import React, { useState, useEffect } from 'react';
import {
    StyleSheet,
    Text,
    TouchableOpacity,
    View,
    Image,
    TextInput,
} from 'react-native';


import AsyncStorage from '@react-native-async-storage/async-storage';
import { useNavigation } from '@react-navigation/core';

import api from '../services/api'


export default function Login() {

    const [email, setEmail] = useState('helena.souza@spmedicalgroup.com.br')
    const [senha, setSenha] = useState('helena123')
    const navigation = new useNavigation();


    realizarLogin = async () => {
        try {
            const resposta = await api.post('/Login', {
                emailUsuario: email,
                senhaUsuario: senha
            })

            if (resposta.status == 200) {

                const token = resposta.data.token

                
                // Redireciona

                await AsyncStorage.setItem('userToken', token);
                navigation.navigate('Lista')
            }

        } catch (error) {
            console.warn(error)
        }
    }

    return (
        <View
            style={StyleSheet.absoluteFillObject}
            style={styles.fundoLogin}
        >
            <View style={styles.loginContainer}>
                <View style={styles.loginWrapper}>

                    <Image
                        source={require('../../assets/images/logo.png')}
                        style={styles.imgLogin}
                    />


                    <View style={styles.inputContainer}>
                        <TextInput
                            placeholder="email"
                            keyboardType="email-address"
                            onChangeText={(campo) => setEmail(campo)}
                            value={email}
                            placeholderTextColor='rgba(9, 9, 9, 0.5)'
                            style={styles.inputLogin}
                        >
                        </TextInput>

                        <TextInput
                            placeholder="senha"
                            keyboardType="default"
                            onChangeText={(campo) => setSenha(campo)}
                            value={senha}
                            placeholderTextColor='rgba(9, 9, 9, 0.5)'
                            style={styles.inputLogin}
                            secureTextEntry={true}>
                        </TextInput>

                        <TouchableOpacity
                            style={styles.btnLogin}
                            onPress={realizarLogin}
                        >
                            <Text style={styles.btnLoginText}>
                                Login
                            </Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </View>
        </View>
    )

}

const styles = StyleSheet.create({

    loginContainer: {
        ...StyleSheet.absoluteFillObject,
        justifyContent: 'center'
    },

    loginWrapper: {
        width: '100%',
        height: 450,
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: 80
    },

    imgLogin: {
        height: 300,
        width: 500,
    },

    fundoLogin: {
        backgroundColor: '#3D8DF2',
        flex: 1,
    },

    inputContainer: {
        height: 160,
        justifyContent: 'space-between'
    },

    inputLogin: {
        width: 229,
        height: 42,
        color: '#000',
        backgroundColor: 'rgba(255, 255, 255, 0.85)',
        borderRadius: 5,
    },

    btnLogin: {
        width: 229,
        height: 42,
        backgroundColor: '#45E1E6',
        borderRadius: 5,
        alignItems: 'center',
        justifyContent: 'center',
        marginTop: 10
    },

    btnLoginText: {
        color: '#fff',
        textTransform: 'uppercase'
    }
})