# DOTS-Assignment

I have used Unity DOTS to squeeze out extra performance for large games with many entities. In this particular game you will probably not notice much difference from regular MonoBehaviour, but if you scale up the amount of entities and their variations, you will notice a larger improvement. 

I added [BurstCompile] to all possible functions to increase performance. The burst compiler helps giving the performance of native code from C#. The burst compiler is, simply put, translating IL/.NET bytecode to native code. 

One of the components of DOTS is the Entity Component System (ECS). The big advantage of using ECS is its use of cache. When comparing to Object Oriented Programming (OOP), OOP has many more cache misses. This is because the CPU loads a cache line (a whole block) from the main memory into the cache. This cache is then thrown out when accessing memory somewhere else. Using ECS, the relevant data is much more tighly packed. Thus, the block of data that is cached is more relevant, hence making the process faster and you use less CPU with less cache misses. 