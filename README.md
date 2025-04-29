Na aplicação a desenvolver pretende-se que seja feito o render de um prisma, ou seja um sólido com uma base (e
topo) poligonal e uma dada altura, gerado procedimentalmente. A função que calcula os vértices do prisma deve
receber como parâmetros a altura do prisma, o raio do polígono da base e o número de lados do polígono da base.
O render dos lados do prisma deve ser implementado com triangle strips, usando vertex buffer e indexação de
vértices. Deve ser aplicada uma textura aos lados do prisma. O topo do prisma também deve ser rendered com
outra textura aplicada. O prisma deve ser colocado em cima de um plano quadrado e deve ser também aplicada
uma textura ao plano.
Pretende-se usar iluminação, portanto os vértices devem ser definidos com normais. As normais dos vértices dos
topos do prisma devem ser perpendiculares à superfície por eles definidos. As normais dos vértices das faces
exteriores do polígono devem ser perpendiculares em relação ao eixo do prisma, de modo que o prisma tenha o
aspecto de um cilindro.
O prisma deve ter movimento controlado pelas teclas das setas. O movimento do prisma deverá ser restrito ao plano
horizontal. As setas frente/trás deverão fazer o prisma avançar/recuar. As setas direita/esquerda deverão fazer o
prisma rodar no eixo vertical, mudando a sua orientação.
